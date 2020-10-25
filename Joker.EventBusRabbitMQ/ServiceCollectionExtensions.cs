using System;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Joker.EventBusRabbitMQ
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMqConnectionFactory(this IServiceCollection services,Action<RabbitMQClientOptions> options)
        {
            var clientOptions = new RabbitMQClientOptions();
            options.Invoke(clientOptions);
            
            services.AddSingleton(serviceProvider =>
            {
                var uri = new Uri(clientOptions.Url);
                return new ConnectionFactory  { Uri = uri };
            });

            return services;
        }
    }
}