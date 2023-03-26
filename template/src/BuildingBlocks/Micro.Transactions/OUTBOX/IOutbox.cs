using $ext_projectname$.Abstractions.Abstractions;
using $ext_projectname$.Messaging;

namespace $safeprojectname$.Outbox;

public interface IOutbox
{
    bool Enabled { get; }
    Task SaveAsync<T>(MessageEnvelope<T> message, CancellationToken cancellationToken = default) where T : IMessage;
    Task PublishUnsentAsync(CancellationToken cancellationToken = default);
    Task PublishAwaitingAsync(CancellationToken cancellationToken = default);
    Task CleanupAsync(DateTime? to = null, CancellationToken cancellationToken = default);
}