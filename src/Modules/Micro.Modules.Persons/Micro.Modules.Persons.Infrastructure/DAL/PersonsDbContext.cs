using Micro.Modules.Persons.Core.Persons.Entities;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Persons.Infrastructure.DAL
{
    internal class PersonsDbContext : DbContext
    {

        public DbSet<Person> Persons { get; set; }


        public PersonsDbContext(DbContextOptions<PersonsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("persons");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}