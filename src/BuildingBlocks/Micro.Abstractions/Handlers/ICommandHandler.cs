using Micro.Abstractions.Abstractions;

namespace Micro.Abstractions.Handlers;

public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
{
    Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}