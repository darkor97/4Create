using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlDatabase.TableConfigurations
{
    public class EmployeeCompany
    {
        public Guid EmployeeId { get; set; }
        public Guid CompanyId { get; set; }
    }

    public static class EmployeeCompanyConfiguration
    {
        public static void SetupEmployeeCompanyManyToManyRelationship(this ModelBuilder builder)
        {
            builder
                .Entity<Employee>()
                .HasMany(x => x.Companies)
                .WithMany(x => x.Employees)
                .UsingEntity<EmployeeCompany>();
        }
    }
}
