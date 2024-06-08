using GameOfLife.Domain.Games;
using GameOfLife.Domain.GameStates;
using GameOfLife.SharedKernel.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GameOfLife.Infrastructure.Database;

public class GameContext(DbContextOptions<GameContext> dbContextOptions)
    : DbContext(dbContextOptions), IUnitOfWork
{
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<GameState> GameStates { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>(entity =>
        {
            entity.Property(e => e.Width).HasColumnType("smallint");
            entity.Property(e => e.Height).HasColumnType("smallint");
            entity.HasMany<GameState>().WithOne(c => c.GameRelation).HasForeignKey(c => c.GameId).HasPrincipalKey(c => c.Id);
        });
    }
}
