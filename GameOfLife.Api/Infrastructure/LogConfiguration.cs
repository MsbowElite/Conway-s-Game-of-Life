using Microsoft.Extensions.Options;
using Serilog;

namespace GameOfLife.Api.Infrastructure;

public static class LogConfiguration
{
    public static void ConfigureLogging(this IConfiguration configuration, IServiceCollection services)
    {
        IConfigurationSection loggingSection = configuration.GetSection(nameof(LoggingSettings));
        var loggingSettings = new LoggingSettings();
        new ConfigureFromConfigurationOptions<LoggingSettings>(loggingSection)
            .Configure(loggingSettings);

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConfiguration(loggingSection);
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        });
    }

    public static void ConfigureSerilog(this IConfiguration configuration)
    {
        IConfigurationSection loggingSection = configuration.GetSection(nameof(LoggingSettings));
        LoggingSettings loggingSettings = new();
        new ConfigureFromConfigurationOptions<LoggingSettings>(loggingSection)
            .Configure(loggingSettings);

        LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration: loggingSection).Enrich
            .FromLogContext().WriteTo
            .Console();

        Log.Logger = loggerConfiguration.CreateLogger();
    }
}
