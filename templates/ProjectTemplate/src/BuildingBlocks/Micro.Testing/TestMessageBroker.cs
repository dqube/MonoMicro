using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using EasyNetQ;
using EasyNetQ.Consumer;
using Humanizer;
using $ext_projectname$.Abstractions.Attributes;
using $ext_projectname$.Contexts.Accessors;
using $ext_projectname$.Contexts.Providers;
using $ext_projectname$.Messaging.Brokers;
using $ext_projectname$.Messaging.RabbitMQ;
using $ext_projectname$.Messaging.RabbitMQ.Internals;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using IMessage = $ext_projectname$.Abstractions.Abstractions.IMessage;

namespace $safeprojectname$;

[ExcludeFromCodeCoverage]
public sealed class TestMessageBroker : IDisposable
{
    private readonly HashSet<string> _queues = new();
    private readonly IBus _bus;
    private readonly MessageTypeRegistry _messageTypeRegistry;
    
    public IMessageBroker MessageBroker { get; }

    public async Task<T> SubscribeAsync<T>(Func<T, CancellationToken, Task>? handler = null, TimeSpan? timeout = null)
        where T : class, IMessage
    {
        _messageTypeRegistry.Register<T>();
        var messageAttribute = typeof(T).GetCustomAttribute<MessageAttribute>() ?? new MessageAttribute();
        var tcs = new TaskCompletionSource<T>();
        var cancelTask = Task.Delay(timeout ?? TimeSpan.FromSeconds(10));

        var queue = $"test.{typeof(T).Name.Underscore()}.{Guid.NewGuid():N}";
        _queues.Add(queue);
        
        _ = _bus.PubSub.SubscribeAsync<T>(string.Empty,
            async (message, cancellationToken) =>
            {
                if (handler is not null)
                {
                    await handler(message, cancellationToken);
                }

                tcs.SetResult(message);
            },
            configuration =>
            {
                configuration.WithQueueName(queue);
                if (!string.IsNullOrWhiteSpace(messageAttribute.Topic))
                {
                    configuration.WithTopic(messageAttribute.Topic);
                }
            });

        await Task.WhenAny(tcs.Task, cancelTask);
        if (tcs.Task.IsCompleted)
        {
            return tcs.Task.Result;
        }

        throw new TimeoutException("Subscriber has timed out.");
    }

    public TestMessageBroker(string connectionString = "host=localhost;port=5672;virtualHost=/;username=guest;password=guest")
    {
        var contextAccessor = new ContextAccessor();
        var messageContextAccessor = new MessageContextAccessor();
        _messageTypeRegistry = new MessageTypeRegistry();

        _bus = RabbitHutch.CreateBus(connectionString,
            register =>
            {
                register.EnableNewtonsoftJson(new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.None,
                    Converters = new List<JsonConverter>
                    {
                        new StringEnumConverter(new CamelCaseNamingStrategy())
                    },
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                register.Register(typeof(IConventions), typeof(CustomConventions));
                register.Register(typeof(IMessageSerializationStrategy), typeof(CustomMessageSerializationStrategy));
                register.Register(typeof(IHandlerCollectionFactory), typeof(CustomHandlerCollectionFactory));
                register.Register(typeof(IMessageTypeRegistry), _messageTypeRegistry);
                register.Register(typeof(IContextAccessor), contextAccessor);
                register.Register(typeof(IMessageContextAccessor), messageContextAccessor);
            });
        
        var client = new RabbitMqBrokerClient(_bus, messageContextAccessor, new NullLogger<RabbitMqBrokerClient>());
        var contextProvider = new ContextProvider(new HttpContextAccessor(), contextAccessor);
        MessageBroker = new MessageBroker(client, contextProvider);
    }

    public void Dispose()
    {
        foreach (var queue in _queues)
        {
            _bus.Advanced.QueueDelete(queue);
        }
    }
}