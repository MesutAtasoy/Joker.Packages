using Joker.Logging.Models;
using Joker.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;

namespace Joker.Logging
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddElkLogging(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddOption<ElkOptions>("elk");
            services.Configure<ElkOptions>(configuration);
            return services;
        }
    }
}
