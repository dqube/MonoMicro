using Humanizer;
using $ext_projectname$.Abstractions.Abstractions;
using Microsoft.Extensions.Logging;

namespace $safeprojectname$.Clients;

internal sealed class DefaultMessageBrokerClient : IMessageBrokerClient
{
    private readonly ILogger<DefaultMessageBrokerClient> _logger;

    public DefaultMessageBrokerClient(ILogger<DefaultMessageBrokerClient> logger)
    {
        _logger = logger;
    }

    public Task SendAsync<T>(MessageEnvelope<T> message, CancellationToken cancellationToken = default)
        where T : IMessage
    {
        var name = message.Message.GetType().Name.Underscore();
        _logger.LogInformation($"Default message broker, message: '{name}', ID: '{message.Context.MessageId}' won't be sent.");
        return Task.CompletedTask;
    }
}