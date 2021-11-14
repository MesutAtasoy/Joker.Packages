using Joker.Logging.Models;
using Serilog;
using Serilog.Events;

namespace Joker.Logging;

public static class LoggerConfigurationExtensions
{
    public static LoggerConfiguration UseSeq(this LoggerConfiguration configuration, Action<SeqLoggerOptions> optionBuilder)
    {
        var loggerOptions = new SeqLoggerOptions();
        optionBuilder.Invoke(loggerOptions);
            
        if(string.IsNullOrEmpty(loggerOptions?.ServiceUrl))
            throw new ArgumentNullException(nameof(loggerOptions.ServiceUrl));

        configuration.WriteTo.Seq(loggerOptions.ServiceUrl)
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning);

        return configuration;
    }
        
    public static LoggerConfiguration UseConsole(this LoggerConfiguration configuration, Action<ConsoleLoggerOptions> optionBuilder)
    {
        var loggerOptions = new ConsoleLoggerOptions();
        optionBuilder.Invoke(loggerOptions);
            
        configuration.WriteTo.Console();
        return configuration;
    }

    public static Serilog.ILogger Create(this LoggerConfiguration configuration)
    {
        return configuration.CreateLogger();
    }
}