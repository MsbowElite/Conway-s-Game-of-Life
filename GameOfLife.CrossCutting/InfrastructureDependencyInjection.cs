using GameOfLife.Domain.Games;
using GameOfLife.Infrastructure.Database;
using GameOfLife.Infrastructure.Repositories;
using GameOfLife.SharedKernel.Abstractions;
using GameOfLife.Infrastructure.Caching;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfLife.CrossCutting;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
            IConfiguration configuration
        )
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(InfrastructureDependencyInjection).Assembly));

        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<GameContext>(
            (sp, options) => options
                .UseNpgsql(connectionString));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<GameContext>());

        services.AddScoped<IGameRepository, GameRepository>();

        services.AddDistributedMemoryCache();
        services.AddSingleton<ICacheService, CacheService>();
    }
}
