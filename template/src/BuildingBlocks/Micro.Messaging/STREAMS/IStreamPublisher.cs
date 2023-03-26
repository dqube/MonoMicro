using $ext_projectname$.Abstractions.Abstractions;

namespace $safeprojectname$.Streams;

public interface IStreamPublisher
{
    Task PublishAsync<T>(string stream, T message, CancellationToken cancellationToken = default) where T : IMessage;
}