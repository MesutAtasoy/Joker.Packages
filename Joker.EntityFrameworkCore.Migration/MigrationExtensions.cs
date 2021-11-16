using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Joker.EntityFrameworkCore.Migration;

public static class MigrationExtensions
{
    public static IHost MigrateDbContext<TContext>(this IHost webHost,
        Action<TContext, IServiceProvider> seeder) where TContext
        : DbContext
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


                var retry = Policy.Handle<SqlException>()
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
    
    public static async Task MigrateDbContextAsync<TContext>(this IApplicationBuilder applicationBuilder,
        Action<TContext, IServiceProvider> seeder) where TContext
        : DbContext
    {
        await using var scope = applicationBuilder.ApplicationServices.CreateAsyncScope();
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<TContext>>();

        var context = services.GetService<TContext>();

        try
        {
            //Test Commit
            logger.LogInformation("Migrating database associated with context {DbContextName}",
                typeof(TContext).Name);


            var retry = Policy.Handle<SqlException>()
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

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
        IServiceProvider services)
        where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }
}