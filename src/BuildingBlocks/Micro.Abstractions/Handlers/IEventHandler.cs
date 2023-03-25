using Micro.Abstractions.Abstractions;

namespace Micro.Abstractions.Handlers;

public interface IEventHandler<in TEvent> where TEvent : class, IEvent
{
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
}