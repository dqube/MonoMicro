using $safeprojectname$.Abstractions;

namespace $safeprojectname$.Handlers;

public interface IEventHandler<in TEvent> where TEvent : class, IEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}