using Micro.Abstractions.Abstractions;

namespace Micro.Abstractions.Handlers;

public interface IQueryDispatcher
{
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}