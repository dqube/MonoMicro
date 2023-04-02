using Micro.Modules.Wallets.Domain.Owners.Entities;
using Micro.Modules.Wallets.Domain.Owners.ValueObjects;
using Micro.Modules.Wallets.Domain.Wallets.Entities;
using Micro.Modules.Wallets.Domain.Wallets.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.Modules.Wallets.Infrastructure.EF.Configurations;

internal class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasIndex(x => new { x.OwnerId, x.Currency }).IsUnique();
        builder.Property(x => x.Version).IsConcurrencyToken();
        builder.HasOne<Owner>().WithMany().HasForeignKey(x => x.OwnerId);

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new WalletId(x));

        builder.Property(x => x.OwnerId)
            .IsRequired()
            .HasConversion(x => x.Value, x => new OwnerId(x));

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasConversion(x => x.Value, x => new Currency(x));
    }
}