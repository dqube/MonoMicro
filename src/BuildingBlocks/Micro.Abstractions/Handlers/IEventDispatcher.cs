using Micro.Abstractions.Abstractions;

namespace Micro.Abstractions.Handlers;

public interface IEventDispatcher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IEvent;
}