using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SqlDatabase.TableConfigurations
{
    internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder
                .HasIndex(x => x.Name)
                .IsUnique();

            builder
                .Property(x => x.CreatedAt)
                .ValueGeneratedOnAdd();
        }
    }
}
