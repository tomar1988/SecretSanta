using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SecretSanta.Factories;
using SecretSanta.Services.Assigners;
using SecretSanta.Services.Readers;

namespace SecretSanta.Common
{
    public class ServiceCollectionConfigurator
    {
        public static IServiceProvider RegisterDependencies(IConfiguration configuration)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .AddTransient<ISecretSantaFactory, SecretSantaFactory>()
                .AddSingleton(configuration)
                .BuildServiceProvider();

            return serviceProvider;
        }

        public static IConfiguration RegisterAppSetting()
        {
          
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            return configuration;
        }
    }
}
