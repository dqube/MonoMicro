using $safeprojectname$.Abstractions;

namespace $safeprojectname$.Handlers;

public interface ICommandDispatcher
{
    Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : class, ICommand;
}