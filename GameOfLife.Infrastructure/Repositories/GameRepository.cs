using GameOfLife.Domain.Games;
using GameOfLife.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GameOfLife.Infrastructure.Repositories;

public sealed class GameRepository(GameContext context) : IGameRepository
{
    public async Task InsertAsync(Game game, CancellationToken cancellationToken)
        => await context.Games.AddAsync(game, cancellationToken);

    public ValueTask<Game?> GetByIdAsync(Guid gameId, CancellationToken cancellationToken)
        => context.Games.FindAsync(new object[] { gameId }, cancellationToken);

    public async Task<bool> AnyByIdAsync(Guid gameId, CancellationToken cancellationToken)
        => await context.Games.AnyAsync(g => g.Id == gameId, cancellationToken);
}
