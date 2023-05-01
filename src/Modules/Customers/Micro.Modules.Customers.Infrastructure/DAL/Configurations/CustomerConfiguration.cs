using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Customers.Core.Customers.Entities;
using Micro.Modules.Customers.Core.Customers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.Modules.Customers.Infrastructure.DAL.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        //builder.HasIndex(x => new { x.OwnerId, x.Currency }).IsUnique();
        //builder.Property(x => x.Version).IsConcurrencyToken();
        //builder.HasOne<Owner>().WithMany().HasForeignKey(x => x.OwnerId);
        builder.ToTable("Customers", "customers");
        builder.HasKey(e => e.Id);
        builder.Property(x => x.Id)
            .HasColumnName(nameof(CustomerId))
            .HasConversion(x => x.Value, x => new CustomerId(x))
            .ValueGeneratedOnAdd();

    }
}