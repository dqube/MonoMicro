using System.Collections.Concurrent;
using Azure.Messaging.ServiceBus;
using Humanizer;
using $ext_projectname$.Abstractions.Abstractions;
using $ext_projectname$.Abstractions.Serialization;
using $ext_projectname$.Contexts.Accessors;
using $safeprojectname$.Internals;
using $ext_projectname$.Messaging.Clients;
using Microsoft.Extensions.Logging;

namespace $safeprojectname$;

internal sealed class AzureServiceBusBrokerClient : IMessageBrokerClient
{
    private readonly ConcurrentDictionary<Type, string> _names = new();
    private readonly ServiceBusClient _client;
    private readonly IBrokerConventions _conventions;
    private readonly IJsonSerializer _serializer;
    private readonly IMessageContextAccessor _messageContextAccessor;
    private readonly ILogger<AzureServiceBusBrokerClient> _logger;

    public AzureServiceBusBrokerClient(ServiceBusClient client, IBrokerConventions conventions, IJsonSerializer serializer,
        IMessageContextAccessor messageContextAccessor, ILogger<AzureServiceBusBrokerClient> logger)
    {
        _client = client;
        _conventions = conventions;
        _serializer = serializer;
        _messageContextAccessor = messageContextAccessor;
        _logger = logger;
    }
    
    public async Task SendAsync<T>(MessageEnvelope<T> messageEnvelope, CancellationToken cancellationToken = default) where T : IMessage
    {
        var messageContext = messageEnvelope.Context;
        _messageContextAccessor.MessageContext = messageContext;
        var messageName = _names.GetOrAdd(typeof(T), typeof(T).Name.Underscore());
        _logger.LogInformation("Sending a message: {MessageName}  [ID: {MessageId}, Activity ID: {ActivityId}]...",
            messageName, messageContext.MessageId, messageContext.Context.ActivityId);

        var topicName = _conventions.GetTopicNamingConvention(typeof(T));
        var sender = _client.CreateSender(topicName);
        var json = _serializer.Serialize(messageEnvelope.Message);

        var serviceBusMessage = new ServiceBusMessage(json);
        serviceBusMessage.MessageId = messageContext.MessageId;
        serviceBusMessage.CorrelationId = messageContext.Context.ActivityId;
        serviceBusMessage.ApplicationProperties.Add("type", typeof(T).Name.Underscore());
        
        await sender.SendMessageAsync(serviceBusMessage, cancellationToken);
    }
}