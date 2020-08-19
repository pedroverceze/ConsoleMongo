using CDBMongo.Data;
using CDBMongo.Data.Settings;
using CDBMongo.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CDBMongo
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json").Build();

            var mongoDbSettings = configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();

            var serviceProvider = new ServiceCollection()
           .AddLogging(log => log.AddConsole())
           .AddMongoDb(settings =>
           {
               settings.ConnectionString = mongoDbSettings.ConnectionString;
               settings.DatabaseName = mongoDbSettings.DatabaseName;
           })
           .AddScoped<IMongoService, MongoService>()
           .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>();


            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogInformation("Starting application");

            //do the actual work here
            var service = serviceProvider.GetRequiredService<IMongoService>();

            await service.MongoHandler();

            logger.LogDebug("Finished with successfully");
            Console.ReadLine();
        }
    }
}
