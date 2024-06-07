using GameOfLife.Domain.Games;
using GameOfLife.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace GameOfLife.Infrastructure.Database;

public class GameContext(DbContextOptions<GameContext> dbContextOptions)
    : DbContext(dbContextOptions), IUnitOfWork
{
    public DbSet<Game> Games { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<Monster>().HasMany<Battle>().WithOne(c => c.MonsterARelation).HasForeignKey(c => c.MonsterA).HasPrincipalKey(c => c.Id);
        //modelBuilder.Entity<Monster>().HasMany<Battle>().WithOne(c => c.MonsterBRelation).HasForeignKey(c => c.MonsterB).HasPrincipalKey(c => c.Id);
        //modelBuilder.Entity<Monster>().HasMany<Battle>().WithOne(c => c.WinnerRelation).HasForeignKey(c => c.MonsterWinnerId).HasPrincipalKey(c => c.Id);
    }
}
