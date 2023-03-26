using $safeprojectname$.Abstractions;

namespace $safeprojectname$.Handlers;

public interface IQueryDispatcher
{
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}