using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Serilog;
using Serilog.Core;
using static Infrastructure.Settings.InfrastructureConfiguration;

namespace Infrastructure.SerilogConfig
{
    public static class SerilogConfig
    {
        internal static Logger SetupSerilog()
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
                .MinimumLevel.Information()
            .CreateLogger();
        }
    }
}
