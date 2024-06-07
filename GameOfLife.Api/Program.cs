using GameOfLife.Api.Endpoints.Internal;
using GameOfLife.Api.Extensions;
using GameOfLife.Api.Infrastructure;
using GameOfLife.Application;
using GameOfLife.CrossCutting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));
builder.Configuration.ConfigureLogging(builder.Services);
builder.Configuration.ConfigureSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseEndpoints<Program>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseRequestContextLogging();
app.UseSerilogRequestLogging();
app.UseExceptionHandler();

app.Run();

public partial class Program { }