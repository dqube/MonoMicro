using $ext_projectname$.Abstractions.Abstractions;

namespace $safeprojectname$.Internals;

public interface IMessageHandler
{
    Task HandleAsync<T>(Func<IServiceProvider, T, CancellationToken, Task> handler, T message,
        CancellationToken cancellationToken = default) where T : IMessage;
}