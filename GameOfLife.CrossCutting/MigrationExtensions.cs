using GameOfLife.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfLife.CrossCutting;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using GameContext dbContext =
            scope.ServiceProvider.GetRequiredService<GameContext>();

        if (CheckIfProviderIsNotInMemory(dbContext))
            dbContext.Database.Migrate();
    }

    /// <summary>
    /// Get provider name and check if it is InMemory
    /// </summary>
    /// <param name="dbContext"></param>
    /// <returns></returns>
    private static bool CheckIfProviderIsNotInMemory(GameContext dbContext) =>
        !string.Equals(dbContext.Database.ProviderName, "Microsoft.EntityFrameworkCore.InMemory", StringComparison.OrdinalIgnoreCase);
}
