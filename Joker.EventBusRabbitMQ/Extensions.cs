using Autofac;
using Joker.EventBus;
using Joker.EventBus.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Joker.Shared.Options;

namespace Joker.EventBusRabbitMQ
{
    public static class Extensions
    {
        private static readonly string SectionName = "eventBus";

        public static IServiceCollection AddEventBus(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var options = configuration.GetOptions<EventBusRabbitMQSettings>(SectionName);
            

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 4;
                if (!string.IsNullOrEmpty(options.EventBusRetryCount))
                    retryCount = int.Parse(options.EventBusRetryCount);

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, options.SubscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var options = configuration.GetOptions<EventBusRabbitMQSettings>(SectionName);


            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = options.EventBusConnection,
                    Port = 5672
                };

                if (!string.IsNullOrEmpty(options.EventBusUserName))                
                    factory.UserName = options.EventBusUserName;

                if (!string.IsNullOrEmpty(options.EventBusPassword))
                    factory.Password = options.EventBusPassword;
                
                var retryCount = 4;
                if (!string.IsNullOrEmpty(options.EventBusRetryCount)) 
                    retryCount = int.Parse(options.EventBusRetryCount);
               

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }
    }
}
