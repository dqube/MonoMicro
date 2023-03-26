using $ext_projectname$.Abstractions.Abstractions;

namespace $safeprojectname$.Brokers;

public interface IMessageBroker
{
    Task SendAsync<T>(T message, CancellationToken cancellationToken = default) where T : IMessage;
}