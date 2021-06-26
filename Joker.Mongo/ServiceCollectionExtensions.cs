using System;
using Joker.Mongo.Context;
using Joker.Mongo.Document;
using Joker.Mongo.Options;
using Joker.Mongo.Repository;
using Joker.Mongo.Repository.Contracts;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Joker.Mongo
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, Action<MongoDbOption> mongoDbOptionBuilder)
        {
            MongoDbOption dbOption = new MongoDbOption();
            mongoDbOptionBuilder.Invoke(dbOption);

            if (string.IsNullOrEmpty(dbOption.ConnectionString))
            {
                throw new ArgumentNullException(nameof(dbOption.ConnectionString));
            }
            
            if (string.IsNullOrEmpty(dbOption.DefaultDatabaseName))
            {
                throw new ArgumentNullException(nameof(dbOption.DefaultDatabaseName));
            }
            
            services.AddSingleton(context => new MongoClient(dbOption.ConnectionString));

            services.AddScoped(context =>
            {
                MongoClient client = context.GetService<MongoClient>();
                
                if (client == null)
                {
                    throw new ArgumentNullException(nameof(client));
                }
                
                return client.GetDatabase(dbOption.DefaultDatabaseName);
            });

            return services;
        }

        public static IServiceCollection AddMongoRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IMongoQueryRepository<>), typeof(MongoQueryRepository<>));
            services.AddScoped(typeof(IMongoCommandRepository<>), typeof(MongoCommandRepository<>));
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            return services;
        }
        
        public static IServiceCollection AddMongoContext<TContext>(this IServiceCollection services)
            where TContext : MongoContext
        {
            services.AddScoped<IMongoContext, TContext>();

            return services;
        }

        public static IServiceCollection AddMongoRepository<TEntity>(this IServiceCollection services) 
            where TEntity: class, IDocument
        {
            services.AddScoped<IMongoRepository<TEntity>>( x =>
            {
                var mongoContext = x.GetService<IMongoContext>();
                return new MongoRepository<TEntity>(mongoContext);
            });

            return services;
        }
    }
}
