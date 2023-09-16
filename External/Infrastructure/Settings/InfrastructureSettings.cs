using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure.Settings
{
    internal static class InfrastructureConfiguration
    {
        private static IConfigurationRoot _configuration = default!;

        internal static IConfigurationRoot Configuration
        {
            get
            {
                if (_configuration is null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)
                        .AddJsonFile("Settings/infrastructuresettings.json")
                        .Build();
                }
                return _configuration;
            }
        }

    }
}
