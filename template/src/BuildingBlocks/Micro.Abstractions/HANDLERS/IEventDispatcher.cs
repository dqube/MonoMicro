using $safeprojectname$.Abstractions;

namespace $safeprojectname$.Handlers;

public interface IEventDispatcher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent;
}