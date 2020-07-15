using System;
using Joker.ElasticSearch.Options;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Joker.ElasticSearch
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJokerElasticService(this IServiceCollection services,
            Action<ElasticSearchOptions> action)
        {
            var elasticSearchOptions = new ElasticSearchOptions();
            action.Invoke(elasticSearchOptions);

            if (elasticSearchOptions?.ConnectionString == null) 
                throw new ArgumentNullException(nameof(elasticSearchOptions.ConnectionString),
                    $"{nameof(elasticSearchOptions.ConnectionString)} can not be null");
            
            if (string.IsNullOrEmpty(elasticSearchOptions?.ConnectionString?.HostUrl))
                throw new ArgumentNullException(nameof(elasticSearchOptions.ConnectionString.HostUrl),
                    $"{nameof(elasticSearchOptions.ConnectionString.HostUrl)} can not be null");
            
            var connectionString = new ConnectionSettings(new Uri(elasticSearchOptions.ConnectionString.HostUrl))
                .DisablePing()
                .SniffOnStartup(false)
                .SniffOnConnectionFault(false);

            if (!string.IsNullOrEmpty(elasticSearchOptions.ConnectionString.Username) && !string.IsNullOrEmpty(elasticSearchOptions.ConnectionString.Password))
                connectionString.BasicAuthentication(elasticSearchOptions.ConnectionString.Username,elasticSearchOptions.ConnectionString.Password);

            var client = new ElasticClient(connectionString);
            services.AddSingleton<IElasticClient>(client);
            return services;
        }
    }
}