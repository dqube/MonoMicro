using Micro.Abstractions.Abstractions;
using Micro.Abstractions.Handlers;
using Micro.Abstractions.Attributes;
using Micro.Transactions.Outbox;

namespace Micro.Transactions.Decorators;

[Decorator]
internal sealed class OutboxInstantSenderEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private readonly IEventHandler<T> _handler;
    private readonly IOutbox _outbox;

    public OutboxInstantSenderEventHandlerDecorator(IEventHandler<T> handler, IOutbox outbox)
    {
        _handler = handler;
        _outbox = outbox;
    }

    public async Task HandleAsync(T @event, CancellationToken cancellationToken = default)
    {
        await _handler.HandleAsync(@event, cancellationToken);
        await _outbox.PublishAwaitingAsync(cancellationToken);
    }
}