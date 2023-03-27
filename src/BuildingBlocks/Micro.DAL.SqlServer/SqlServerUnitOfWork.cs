using Microsoft.EntityFrameworkCore;

namespace Micro.DAL.SqlServer;

public abstract class SqlServerUnitOfWork<T> : IUnitOfWork where T : DbContext
{
    private readonly T _dbContext;

    protected SqlServerUnitOfWork(T dbContext)
    {
        _dbContext = dbContext;
    }
   
    public async Task ExecuteAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await action();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}