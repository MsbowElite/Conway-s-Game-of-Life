using GameOfLife.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace GameOfLife.Api.Test;

public class ApiApplication : WebApplicationFactory<Program>
{
    /// <summary>
    /// Remove the standard Database injection and replace by InMemory
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var root = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<GameContext>));
            services.AddDbContext<GameContext>(options =>
                options.UseInMemoryDatabase("Testing", root));
        });

        return base.CreateHost(builder);
    }
}
