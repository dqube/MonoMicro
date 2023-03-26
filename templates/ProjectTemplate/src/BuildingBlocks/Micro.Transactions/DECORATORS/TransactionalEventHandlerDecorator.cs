using $ext_projectname$.Abstractions.Abstractions;
using $ext_projectname$.Abstractions.Handlers;
using $ext_projectname$.Abstractions.Attributes;
using $ext_projectname$.DAL.Postgres;

namespace $safeprojectname$.Decorators;

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