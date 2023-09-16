using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.SqlDatabase.TableConfigurations
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Title)
                .HasConversion(x => x.ToString(), x => (Title)Enum.Parse(typeof(Title), x))
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasIndex(x => x.Title)
                .IsUnique();

            builder
                .Property(x => x.Email)
                .HasMaxLength(512)
                .IsRequired();

            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            builder
                .Property(x => x.CreatedAt)
                .ValueGeneratedOnAdd();
        }
    }
}
