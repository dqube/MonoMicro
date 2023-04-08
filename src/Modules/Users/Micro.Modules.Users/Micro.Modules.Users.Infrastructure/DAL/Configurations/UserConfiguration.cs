using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Users.Core.Users.Entities;
using Micro.Modules.Users.Core.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.Modules.Users.Infrastructure.DAL.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //builder.HasIndex(x => new { x.OwnerId, x.Currency }).IsUnique();
            //builder.Property(x => x.Version).IsConcurrencyToken();
            //builder.HasOne<Owner>().WithMany().HasForeignKey(x => x.OwnerId);
            builder.ToTable("Users", "users");
            builder.Ignore(c => c.DomainEventVersion);
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new UserId(x))
                .ValueGeneratedOnAdd();

        }
    }
}