using GameOfLife.Domain.GameStates;
using GameOfLife.Infrastructure.Database;

namespace GameOfLife.Infrastructure.Repositories;

public sealed class GameStateRepository(GameContext context) : IGameStateRepository
{
    public async Task InsertAsync(GameState gameState, CancellationToken cancellationToken)
    {
        await context.GameStates.AddAsync(gameState, cancellationToken);
    }
}
