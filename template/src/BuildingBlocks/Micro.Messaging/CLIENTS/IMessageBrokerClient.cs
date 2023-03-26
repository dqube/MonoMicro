using $ext_projectname$.Abstractions.Abstractions;

namespace $safeprojectname$.Clients;

public interface IMessageBrokerClient
{
    Task SendAsync<T>(MessageEnvelope<T> messageEnvelope, CancellationToken cancellationToken = default)
        where T : IMessage;
}