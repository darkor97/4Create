using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Serilog;
using Serilog.Core;
using static Infrastructure.Settings.InfrastructureConfiguration;

namespace Infrastructure.SerilogConfig
{
    public static class SerilogConfig
    {
        public static void AddSerilog(this IHostBuilder builder)
        {
            Log.Logger = SetupSerilog();
            builder.UseSerilog();
        }

        private static Logger SetupSerilog()
        {
            return new LoggerConfiguration()
                .WriteTo.MongoDBBson(config =>
                {
                    var configSection = Configuration.GetRequiredSection("Serilog");

                    var dbSettings = new MongoClientSettings()
                    {
                        UseTls = false,
                        Credential = MongoCredential.CreateCredential(configSection["AdminDatabase"], configSection["Username"], configSection["Password"]),
                        Server = new MongoServerAddress(configSection["Host"])
                    };

                    var dbInstance = new MongoClient(dbSettings).GetDatabase(configSection["Database"]);

                    config.SetMongoDatabase(dbInstance);
                    config.SetCollectionName(configSection["Collection"]!);
                })
                .MinimumLevel.Error()
                .CreateLogger();
        }
    }
}
