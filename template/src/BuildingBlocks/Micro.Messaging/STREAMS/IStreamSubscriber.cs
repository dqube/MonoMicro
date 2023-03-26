using $ext_projectname$.Abstractions.Abstractions;

namespace $safeprojectname$.Streams;

public interface IStreamSubscriber
{
    Task SubscribeAsync<T>(string stream, Func<T, Task> handler, CancellationToken cancellationToken = default)
        where T : class, IMessage;
}