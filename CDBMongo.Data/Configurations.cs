using CDBMongo.Data.Repositories;
using CDBMongo.Data.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace CDBMongo.Data
{
    public static class Configurations
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, Action<MongoDbSettings> settings)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            services.Configure<MongoDbSettings>(settings);

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            return services;
        }
    }
}
