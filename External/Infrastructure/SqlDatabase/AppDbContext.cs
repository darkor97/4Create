using Domain.Entities;
using Infrastructure.SqlDatabase.TableConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlDatabase
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<EmployeeCompany> EmployeeCompanies { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.SetupEmployeeCompanyManyToManyRelationship();
        }
    }
}
