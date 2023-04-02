using Micro.DAL.SqlServer;

namespace Micro.Modules.Wallets.Infrastructure.EF;

internal class WalletsUnitOfWork : SqlServerUnitOfWork<WalletsDbContext>
{
    public WalletsUnitOfWork(WalletsDbContext dbContext) : base(dbContext)
    {
    }
}