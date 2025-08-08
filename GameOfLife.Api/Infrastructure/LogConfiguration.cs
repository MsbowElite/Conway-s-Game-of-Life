using System.Globalization;
using Microsoft.Extensions.Options;
using Serilog;

namespace GameOfLife.Api.Infrastructure;

internal static class LogConfiguration
{
    public static void ConfigureSerilog(this IConfiguration configuration)
    {
        IConfigurationSection loggingSection = configuration.GetSection(nameof(LoggingSettings));
        LoggingSettings loggingSettings = new();
        new ConfigureFromConfigurationOptions<LoggingSettings>(loggingSection)
            .Configure(loggingSettings);

        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration: loggingSection)
            .Enrich.FromLogContext()
            .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture); // Fix: Provide IFormatProvider

        Log.Logger = loggerConfiguration.CreateLogger();
    }
}
