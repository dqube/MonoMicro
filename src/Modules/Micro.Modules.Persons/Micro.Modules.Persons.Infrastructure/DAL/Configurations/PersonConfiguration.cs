using Micro.Abstractions.Kernel.Types;
using Micro.Modules.Persons.Core.Persons.Entities;
using Micro.Modules.Persons.Core.Persons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Micro.Modules.Persons.Infrastructure.DAL.Configurations
{
    internal class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            //builder.HasIndex(x => new { x.OwnerId, x.Currency }).IsUnique();
            //builder.Property(x => x.Version).IsConcurrencyToken();
            //builder.HasOne<Owner>().WithMany().HasForeignKey(x => x.OwnerId);
            builder.ToTable("Persons", "persons");
            builder.Ignore(c => c.DomainEventVersion);
            builder.HasKey(e => e.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new PersonId(x))
                .ValueGeneratedOnAdd();

        }
    }
}