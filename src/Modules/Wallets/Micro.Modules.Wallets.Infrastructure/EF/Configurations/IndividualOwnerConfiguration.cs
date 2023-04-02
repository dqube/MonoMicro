using Micro.Modules.Wallets.Domain.Owners.Entities;
using Micro.Modules.Wallets.Domain.Owners.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inflow.Modules.Wallets.Infrastructure.EF.Configurations;

internal class IndividualOwnerConfiguration : IEntityTypeConfiguration<IndividualOwner>
{
    public void Configure(EntityTypeBuilder<IndividualOwner> builder)
    {
        builder.Property(x => x.FullName)
            .IsRequired()
            .HasConversion(x => x.Value, x => new FullName(x));
    }
}