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

        public static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration, string AppName)
        {
            var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ApplicationName", AppName)
            .WriteTo.Elasticsearch(
                new ElasticsearchSinkOptions(new Uri(configuration["elk:Url"]))
                {
                    AutoRegisterTemplate = true,
                    IndexFormat = configuration["elk:indexFormat"],
                    CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
                });

            var isEnabled = configuration.GetValue<bool>("logger:enabled");
            if (isEnabled)
                logger.WriteTo.MSSqlServer(configuration["logger:connectionString"], configuration["logger:tableName"]);

            return logger.CreateLogger();
        }
    }
}
