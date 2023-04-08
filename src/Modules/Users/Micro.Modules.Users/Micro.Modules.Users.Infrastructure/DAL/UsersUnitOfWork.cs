using Micro.DAL.SqlServer;

namespace Micro.Modules.Users.Infrastructure.DAL
{
    internal class UsersUnitOfWork : SqlServerUnitOfWork<UsersDbContext>
    {
        public UsersUnitOfWork(UsersDbContext dbContext) : base(dbContext)
        {
        }
    }
}