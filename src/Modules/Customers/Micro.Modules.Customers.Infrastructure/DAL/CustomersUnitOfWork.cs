using Micro.DAL.SqlServer;

namespace Micro.Modules.Customers.Infrastructure.DAL;

internal class CustomersUnitOfWork : SqlServerUnitOfWork<CustomersDbContext>
{
    public CustomersUnitOfWork(CustomersDbContext dbContext) : base(dbContext)
    {
    }
}