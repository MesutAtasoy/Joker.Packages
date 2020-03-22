using Joker.Logging.Models;
using Joker.Shared.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using System;

namespace Joker.Logging
{
    public static class IApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>()
                ?? throw new ArgumentException("Missing dependency", nameof(ILoggerFactory));

            var elkOptionValue = app.ApplicationServices.GetRequiredService<IOptions<ElkOptions>>()
                ?? throw new ArgumentException("Missing dependency", nameof(IOptions<ElkOptions>));

            var appSettingsValue = app.ApplicationServices.GetRequiredService<IOptions<AppSettings>>()
             ?? throw new ArgumentException("Missing dependency", nameof(IOptions<ElkOptions>));

            var elkOption = elkOptionValue.Value;
            var appSettings = appSettingsValue.Value;

            if (!elkOption.Enabled)
                return app;

            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .Enrich.WithExceptionDetails()
              .Enrich.WithProperty("ApplicationName", appSettings.Name)
              .WriteTo.Elasticsearch(
                  new ElasticsearchSinkOptions(new Uri(elkOption.Url))
                  {
                      AutoRegisterTemplate = true,
                      IndexFormat = elkOption.IndexFormat,
                      CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
                  })
              .CreateLogger();

            loggerFactory.AddSerilog();
            return app;
        }
    }
}
