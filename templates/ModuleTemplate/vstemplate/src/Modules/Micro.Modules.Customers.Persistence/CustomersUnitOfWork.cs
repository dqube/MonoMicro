using Micro.DAL.SqlServer;

namespace $safeprojectname$;

internal class CustomersUnitOfWork : SqlServerUnitOfWork<CustomersDbContext>
{
    public CustomersUnitOfWork(CustomersDbContext dbContext) : base(dbContext)
    {
    }
}
