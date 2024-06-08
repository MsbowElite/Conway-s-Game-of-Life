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

        //if (!dbContext.Database.IsInMemory())
        dbContext.Database.Migrate();
    }
}
