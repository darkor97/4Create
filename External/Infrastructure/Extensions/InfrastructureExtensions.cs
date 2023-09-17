using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Repository;
using Infrastructure.SqlDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using static Infrastructure.SerilogConfig.SerilogConfig;
using static Infrastructure.Settings.InfrastructureConfiguration;

namespace Infrastructure.DependencyExtension
{
    public static class InfrastructureExtensions
    {
        public static void AddSerilog(this IHostBuilder builder)
        {
            Log.Logger = SetupSerilog();
            builder.UseSerilog();
        }

        public static void RegisterInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var conString = Configuration.GetConnectionString("MySqlConnectionString");

                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("Migration")))
                {
                    conString = Configuration.GetConnectionString("MigrationConnectionString");
                }
                options
                    .UseMySql(conString, ServerVersion.AutoDetect(conString), options => options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(15), null))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddTransient<IRepository<Employee>, EmployeeRepository>();
            services.AddTransient<IRepository<Company>, CompanyRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
