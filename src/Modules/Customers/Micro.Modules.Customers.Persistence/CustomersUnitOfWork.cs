using Micro.DAL.SqlServer;

namespace Micro.Modules.Customers.Persistence;

internal class CustomersUnitOfWork : SqlServerUnitOfWork<CustomersDbContext>
{
    public CustomersUnitOfWork(CustomersDbContext dbContext) : base(dbContext)
    {
    }
}
