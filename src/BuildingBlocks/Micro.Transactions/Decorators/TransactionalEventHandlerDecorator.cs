using Micro.Abstractions.Abstractions;
using Micro.Abstractions.Handlers;
using Micro.Abstractions.Attributes;
using Micro.DAL.Postgres;

namespace Micro.Transactions.Decorators;

[Decorator]
internal sealed class TransactionalEventHandlerDecorator<T> : IEventHandler<T> where T : class, IEvent
{
    private readonly IEventHandler<T> _handler;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionalEventHandlerDecorator(IEventHandler<T> handler, IUnitOfWork unitOfWork)
    {
        _handler = handler;
        _unitOfWork = unitOfWork;
    }

    public Task HandleAsync(T @event, CancellationToken cancellationToken = default)
        => _unitOfWork.ExecuteAsync(() => _handler.HandleAsync(@event, cancellationToken), cancellationToken);
}