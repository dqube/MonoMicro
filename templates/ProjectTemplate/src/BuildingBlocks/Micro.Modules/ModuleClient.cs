using $ext_projectname$.Abstractions;
using $ext_projectname$.Abstractions.Attributes;
using $ext_projectname$.Abstractions.Modules;
using System.Collections.Concurrent;
using System.Reflection;

namespace $safeprojectname$;

public sealed class ModuleClient : IModuleClient
{
    private readonly ConcurrentDictionary<Type, MessageAttribute> _messages = new();
    private readonly IModuleRegistry _moduleRegistry;
    private readonly IModuleSerializer _moduleSerializer;
    public ModuleClient(IModuleRegistry moduleRegistry, IModuleSerializer moduleSerializer)
    {
        _moduleRegistry = moduleRegistry;
        _moduleSerializer = moduleSerializer;
    }

    public Task SendAsync(string path, object request, CancellationToken cancellationToken = default)
        => SendAsync<object>(path, request, cancellationToken);

    public async Task<TResult> SendAsync<TResult>(string path, object request,
        CancellationToken cancellationToken = default) where TResult : class
    {
        var registration = _moduleRegistry.GetRequestRegistration(path);
        if (registration is null)
        {
            throw new InvalidOperationException($"No action has been defined for path: '{path}'.");
        }

        var receiverRequest = TranslateType(request, registration.RequestType);
        var result = await registration.Action(receiverRequest, cancellationToken);

        return result is null ? null : TranslateType<TResult>(result);
    }

    public async Task PublishAsync(object message, CancellationToken cancellationToken = default)
    {
        var key = message.GetType().Name;
        var registrations = _moduleRegistry.GetBroadcastRegistrations(key);
        var tasks = new List<Task>();

        foreach (var registration in registrations)
        {
            var action = registration.Action;
            var receiverMessage = TranslateType(message, registration.ReceiverType);
            tasks.Add(action(receiverMessage, cancellationToken));
        }
        await Task.WhenAll(tasks);
    }

    private T TranslateType<T>(object value)
        => _moduleSerializer.Deserialize<T>(_moduleSerializer.Serialize(value));

    private object TranslateType(object value, Type type)
        => _moduleSerializer.Deserialize(_moduleSerializer.Serialize(value), type);
}