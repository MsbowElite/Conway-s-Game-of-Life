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
            entity.HasMany<GameState>().WithOne(c => c.Game).HasForeignKey(c => c.GameId).HasPrincipalKey(c => c.Id);

            entity.HasOne(e => e.GameState)
                .WithOne(e => e.Game)
                .HasForeignKey<Game>(e => e.FinalGameStateId)
                .HasConstraintName("GameStateFinal_FK");
        });

        modelBuilder.Entity<GameState>(entity =>
        {
            entity.Property(e => e.GenerationNumber).HasColumnType("smallint");
            entity.HasOne(e => e.GameRelation)
                .WithMany(e => e.GameStates)
                .HasForeignKey(e => e.GameId)
                .HasConstraintName("Game_FK");
        });
    }
}
