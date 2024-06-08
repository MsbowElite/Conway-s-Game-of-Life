using GameOfLife.Domain.Games;
using GameOfLife.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GameOfLife.Infrastructure.Repositories;

public sealed class GameRepository(GameContext context) : IGameRepository
{
    public async Task InsertAsync(Game game, CancellationToken cancellationToken)
    {
        await context.Games.AddAsync(game, cancellationToken);
    }

    public async Task<Game> GetByIdAsync(Guid gameId, CancellationToken cancellationToken)
    {
        return await context.Games.FindAsync(gameId, cancellationToken);
    }

    public async Task<bool> AnyByIdAsync(Guid gameId, CancellationToken cancellationToken)
    {
        return await context.Games.AnyAsync(g => g.Id == gameId, cancellationToken);
    }
}
