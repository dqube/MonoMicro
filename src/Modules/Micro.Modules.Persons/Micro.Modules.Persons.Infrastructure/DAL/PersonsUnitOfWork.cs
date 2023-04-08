using Micro.DAL.SqlServer;

namespace Micro.Modules.Persons.Infrastructure.DAL
{
    internal class PersonsUnitOfWork : SqlServerUnitOfWork<PersonsDbContext>
    {
        public PersonsUnitOfWork(PersonsDbContext dbContext) : base(dbContext)
        {
        }
    }
}