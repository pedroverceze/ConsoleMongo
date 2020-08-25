using CDBMongo.Data.Repositories;
using CDBMongo.Data.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System;

namespace CDBMongo.Data
{
    public static class Configurations
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, Action<MongoDbSettings> settings)
        {
            services.Configure<MongoDbSettings>(settings);

            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            return services;
        }
    }
}
