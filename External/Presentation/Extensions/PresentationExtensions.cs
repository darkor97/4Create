using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Models;

namespace Presentation.Extensions
{
    public static class PresentationExtensions
    {
        public static void RegisterPresentationDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.CreateMap<EmployeeRequest, Employee>();
            });
        }
    }
}
