using GameOfLife.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameOfLife.CrossCutting;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using GameContext dbContext =
            scope.ServiceProvider.GetRequiredService<GameContext>();

        if (dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
        {
            dbContext.Database.Migrate();
        }
    }

    //public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    //{
    //    // Fix: Ensure the GetValue extension method is available by adding the required using directive
    //    if (configuration.GetValue<bool>("UseInMemoryDatabase"))
    //    {
    //        services.AddDbContext<GameContext>(options =>
    //            options.UseInMemoryDatabase("GameOfLife"));
    //    }
    //    else
    //    {
    //        services.AddDbContext<GameContext>(options =>
    //            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    //    }
    //}
}

