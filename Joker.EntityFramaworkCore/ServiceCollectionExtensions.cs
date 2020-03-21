using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Joker.EntityFramaworkCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJokerDbContext<TContext>(this IServiceCollection services,
            Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction)
            where TContext : DbContext
        {

            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"], sqlServerOptionsAction);

            });
            return services;
        }

        public static IServiceCollection AddJokerDbContext<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {

            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            string assemblyName = typeof(TContext).Namespace;

            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"], sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(assemblyName);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);

                }
                );
            });
            return services;
        }
    }
}

