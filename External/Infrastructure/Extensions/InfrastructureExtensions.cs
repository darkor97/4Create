using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Repository;
using Infrastructure.SqlDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Infrastructure.Settings.InfrastructureConfiguration;

namespace Infrastructure.DependencyExtension
{
    public static class InfrastructureExtensions
    {
        public static void RegisterInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var conString = Configuration.GetConnectionString("MySqlConnectionString");
                options
                    .UseMySql(conString, ServerVersion.AutoDetect(conString), options => options.EnableRetryOnFailure())
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddTransient<IRepository<Employee>, EmployeeRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}
