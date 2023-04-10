using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Application
{
    public class AddressConfiguration:IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            // ID
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            // AddressType
            builder.Property(x => x.AddressType).IsRequired();
            builder.Property(x => x.AddressType).HasConversion<int>();

            // Name
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100);

            // District
            builder.Property(x => x.District).IsRequired();
            builder.Property(x => x.District).HasMaxLength(100);

            // PostCode
            builder.Property(x => x.PostCode).IsRequired();
            builder.Property(x => x.PostCode).HasMaxLength(20);

            // AddressLine1
            builder.Property(x => x.AddressLine1).IsRequired();

            // AddressLine2
            builder.Property(x => x.AddressLine2).IsRequired(false);

            // Common Fields

            // CreatedOn
            builder.Property(x => x.CreatedOn).IsRequired();

            // CreatedByUserId
            builder.Property(x => x.CreatedByUserId).IsRequired(false);
            builder.Property(x => x.CreatedByUserId).HasMaxLength(100);

            // ModifiedOn
            builder.Property(x => x.ModifiedOn).IsRequired(false);

            // ModifiedByUserId
            builder.Property(x => x.ModifiedByUserId).IsRequired(false);
            builder.Property(x => x.ModifiedByUserId).HasMaxLength(100);

            // DeletedOn
            builder.Property(x => x.DeletedOn).IsRequired(false);

            // DeletedByUserId
            builder.Property(x => x.DeletedByUserId).IsRequired(false);
            builder.Property(x => x.DeletedByUserId).HasMaxLength(100);

            // IsDeleted
            builder.Property(x => x.IsDeleted).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValueSql("0");
            builder.HasIndex(x => x.IsDeleted);



            // Relationships
            builder.HasOne<User>().WithMany()
                .HasForeignKey(x => x.UserId);


            builder.HasOne(x => x.Country)
                .WithOne()
                .HasForeignKey<Country>(x => x.Id);

            builder.HasOne(x => x.City)
               .WithOne()
               .HasForeignKey<City>(x => x.Id);

            builder.ToTable("Addresses");

        }
    }
}
