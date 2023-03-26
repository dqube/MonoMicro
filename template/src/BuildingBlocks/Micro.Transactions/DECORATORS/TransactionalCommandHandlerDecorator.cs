using $ext_projectname$.Abstractions.Abstractions;
using $ext_projectname$.Abstractions.Handlers;
using $ext_projectname$.Abstractions.Attributes;
using $ext_projectname$.DAL.Postgres;

namespace $safeprojectname$.Decorators;

[Decorator]
internal sealed class TransactionalCommandHandlerDecorator<T> : ICommandHandler<T> where T : class, ICommand
{
    private readonly ICommandHandler<T> _handler;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionalCommandHandlerDecorator(ICommandHandler<T> handler, IUnitOfWork unitOfWork)
    {
        _handler = handler;
        _unitOfWork = unitOfWork;
    }

    public Task HandleAsync(T command, CancellationToken cancellationToken = default)
        => _unitOfWork.ExecuteAsync(() => _handler.HandleAsync(command, cancellationToken), cancellationToken);
}