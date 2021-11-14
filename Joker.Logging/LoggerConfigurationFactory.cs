using Joker.Logging.Models;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Joker.Logging;

public class LoggerConfigurationFactory
{
    public static ILogger CreateSerilogLogger(IConfiguration configuration, Action<SeqLoggerOptions> optionBuilder)
    {
        var loggerOptions = new SeqLoggerOptions();
        optionBuilder.Invoke(loggerOptions);
            
        string applicationName = configuration["Serilog:ApplicationName"];
            
        return new LoggerConfiguration()
            .Enrich.WithProperty("ApplicationContext", loggerOptions.AppName)
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}