using GameOfLife.Api.Endpoints.Internal;
using GameOfLife.Api.Extensions;
using GameOfLife.Api.Infrastructure;
using GameOfLife.Application;
using GameOfLife.CrossCutting;
using GameOfLife.SharedKernel;
using Microsoft.Extensions.Options;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Logging.ClearProviders();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Configuration.ConfigureSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

IConfigurationSection gameStateConfigSection = builder.Configuration.GetSection(nameof(GameStateConfig));
var gameStateConfigSettings = new GameStateConfig();
new ConfigureFromConfigurationOptions<GameStateConfig>(gameStateConfigSection)
    .Configure(gameStateConfigSettings);
builder.Services.AddSingleton(gameStateConfigSettings);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

WebApplication app = builder.Build();

app.UseEndpoints<Program>();

app.UseSwagger();
app.UseSwaggerUI();

app.ApplyMigrations();

app.UseRequestContextLogging();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();

app.Run();

#pragma warning disable CA1515 // Make it accessible by tests
public partial class Program { }
#pragma warning restore CA1515
