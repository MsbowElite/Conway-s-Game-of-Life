using GameOfLife.Domain.Games;
using GameOfLife.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GameOfLife.Infrastructure.Database;

public class GameContext(DbContextOptions<GameContext> dbContextOptions)
    : DbContext(dbContextOptions), IUnitOfWork
{
    public DbSet<Game> Games { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
