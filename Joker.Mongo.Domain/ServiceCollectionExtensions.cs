using Joker.Mongo.Domain.Context;
using Joker.Mongo.Domain.Repository;
using Joker.Mongo.Domain.Repository.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Joker.Mongo.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDomainRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IMongoDomainQueryRepository<>), typeof(MongoDomainQueryRepository<>));
            services.AddScoped(typeof(IMongoDomainCommandRepository<>), typeof(MongoDomainCommandRepository<>));
            services.AddScoped(typeof(IMongoDomainRepository<>), typeof(MongoDomainDomainRepository<>));

            return services;
        }
        
        public static IServiceCollection AddMongoContext<TContext>(this IServiceCollection services)
            where TContext : MongoDomainContext
        {
            services.AddScoped<IMongoDomainContext, TContext>();

            return services;
        }

    }
}
