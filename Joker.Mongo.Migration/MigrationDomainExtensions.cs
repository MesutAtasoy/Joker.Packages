using System;
using Joker.Mongo.Domain.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;

namespace Joker.Mongo.Migration
{
    public static class MigrationDomainExtensions
    {
        public static IHost MigrateDbContext<TContext>(this IHost webHost,
            Action<TContext, IServiceProvider> seeder) where TContext
            : MongoDomainContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var logger = services.GetRequiredService<ILogger<TContext>>();

                var context = services.GetService<TContext>();

                try
                {
                    //Test Commit
                    logger.LogInformation("Migrating database associated with context {DbContextName}",
                        typeof(TContext).Name);


                    var retry = Policy.Handle<Exception>()
                        .WaitAndRetry(new TimeSpan[]
                        {
                            TimeSpan.FromSeconds(3),
                            TimeSpan.FromSeconds(5),
                            TimeSpan.FromSeconds(8),
                        });

                    retry.Execute(() => InvokeSeeder(seeder, context, services));


                    logger.LogInformation("Migrated database associated with context {DbContextName}",
                        typeof(TContext).Name);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}",
                        typeof(TContext).Name);
                }
            }

            return webHost;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
            IServiceProvider services)
            where TContext : MongoDomainContext
        {
            seeder(context, services);
        }
    }
}