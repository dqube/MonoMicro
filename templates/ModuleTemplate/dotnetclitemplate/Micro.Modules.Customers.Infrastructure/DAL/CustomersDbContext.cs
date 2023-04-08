using Micro.Modules.Customers.Core.Customers.Entities;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Customers.Infrastructure.DAL;

internal class CustomersDbContext : DbContext
{

    public DbSet<Customer> Customers { get; set; }


    public CustomersDbContext(DbContextOptions<CustomersDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("customers");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
