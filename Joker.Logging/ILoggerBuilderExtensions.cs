using Elastic.Apm.SerilogEnricher;
using Joker.Logging.Models;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Joker.Logging;

public static class LoggerBuilder
{
    public static Serilog.ILogger CreateLoggerElasticSearch(Action<ElkOptions> optionBuilder)
    {
        var elkOptions = new ElkOptions();
        optionBuilder.Invoke(elkOptions);
        
        var logConfiguration = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
            .Enrich.WithClientIp()
            .Enrich.WithClientAgent()
            .Enrich.WithSpan()
            .Enrich.WithElasticApmCorrelationInfo()
            .Enrich.WithProperty("Environment", elkOptions.EnvironmentName)
            .ReadFrom.Configuration(elkOptions.Configuration)
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elkOptions.Url))
            {
                AutoRegisterTemplate = true,
                IndexFormat = elkOptions.IndexFormat,
                CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true)
            });
     

        return logConfiguration.CreateLogger();
    }
    
    public static Serilog.ILogger CreateLoggerConsole(Action<ConsoleLoggerOptions> optionBuilder)
    {
        var loggerOptions = new ConsoleLoggerOptions();
        optionBuilder.Invoke(loggerOptions);

        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ApplicationName", loggerOptions.AppName)
            .UseConsole(optionBuilder);

        return logger.CreateLogger();
    }
    
    public static Serilog.ILogger CreateLoggerSeq(Action<SeqLoggerOptions> optionBuilder)
    {
        var loggerOptions = new SeqLoggerOptions();
        optionBuilder.Invoke(loggerOptions);

        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ApplicationName", loggerOptions.AppName)
            .UseSeq(optionBuilder);

        return logger.CreateLogger();
    }
    public static LoggerConfiguration CreateLoggerInstance(Action<LoggerOptions> optionBuilder)
    {
        var loggerOptions = new LoggerOptions();
        optionBuilder.Invoke(loggerOptions);
            
        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ApplicationName", loggerOptions.AppName);
            
        return logger;
    }
}