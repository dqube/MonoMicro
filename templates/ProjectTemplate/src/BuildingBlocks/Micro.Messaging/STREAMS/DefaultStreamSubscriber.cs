using $ext_projectname$.Abstractions.Abstractions;

namespace $safeprojectname$.Streams;

internal sealed class DefaultStreamSubscriber : IStreamSubscriber
{
    public Task SubscribeAsync<T>(string stream, Func<T, Task> handler, CancellationToken cancellationToken = default)
        where T : class, IMessage => Task.CompletedTask;
}