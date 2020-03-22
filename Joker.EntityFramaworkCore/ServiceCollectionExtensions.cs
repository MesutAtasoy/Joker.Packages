using Joker.EntityFramaworkCore.OptionsBuilders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Joker.EntityFramaworkCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJokerDbContext<TContext>(this IServiceCollection services, Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction)
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

        public static IServiceCollection AddJokerDbContext<TContext>(this IServiceCollection services, Action<JokerDbContextOptionBuilder> optionBuilder)
           where TContext : DbContext
        {
            JokerDbContextOptionBuilder contextOptionBuilder = new JokerDbContextOptionBuilder();
            optionBuilder.Invoke(contextOptionBuilder);

            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            if (string.IsNullOrEmpty(contextOptionBuilder.ConnectionString))
                throw new ArgumentNullException("Connectionstring can not be null", nameof(contextOptionBuilder.ConnectionString));

            string assemblyName = typeof(TContext).Namespace;

            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(contextOptionBuilder.ConnectionString, sqlOptions =>
                {
                    if (contextOptionBuilder.EnableMigration)
                    {
                        sqlOptions.MigrationsAssembly(assemblyName);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: contextOptionBuilder.MaxRetryCount,
                            maxRetryDelay: contextOptionBuilder.MaxRetryDelay ?? TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddJokerNpDbContext<TContext>(this IServiceCollection services, Action<JokerDbContextOptionBuilder> optionBuilder)
         where TContext : DbContext
        {
            JokerDbContextOptionBuilder contextOptionBuilder = new JokerDbContextOptionBuilder();
            optionBuilder.Invoke(contextOptionBuilder);

            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            if (string.IsNullOrEmpty(contextOptionBuilder.ConnectionString))
                throw new ArgumentNullException("Connectionstring can not be null", nameof(contextOptionBuilder.ConnectionString));

            string assemblyName = typeof(TContext).Namespace;

            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(contextOptionBuilder.ConnectionString, sqlOptions =>
                {
                    if (contextOptionBuilder.EnableMigration)
                        sqlOptions.MigrationsAssembly(assemblyName);
                });
            });

            return services;
        }
    }
}

