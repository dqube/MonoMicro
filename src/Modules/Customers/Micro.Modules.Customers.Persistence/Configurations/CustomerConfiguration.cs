using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Customers.Domain.Entities;
using Micro.Modules.Customers.Persistence.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.Modules.Customers.Persistence.Configurations;


internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.ToTable(TableNames.Customers);

        entity.HasKey(e => e.Id).HasName("PK__Table__3214EC07212A7551");

        entity.Property(e => e.Id).ValueGeneratedNever();
        entity.Property(e => e.Email).HasMaxLength(50);
        entity.Property(e => e.FirstName).HasMaxLength(50);
        entity.Property(e => e.LastName).HasMaxLength(50);
        entity.Property(e => e.Name).HasMaxLength(150);
    }

}

