using GameOfLife.Domain.GameStates;
using GameOfLife.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GameOfLife.Infrastructure.Repositories;

public sealed class GameStateRepository(GameContext context) : IGameStateRepository
{
    public async Task InsertAsync(GameState gameState, CancellationToken cancellationToken)
    {
        await context.GameStates.AddAsync(gameState, cancellationToken);
    }

    public async Task<GameState> GetByGameIdAndGenerationNumberAsync(
        Guid gameId, ushort generationNumber, CancellationToken cancellationToken)
    {
        return await context.GameStates.Where(
            gs => gs.GameId == gameId && gs.GenerationNumber == generationNumber)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<GameState> GetLastByGameId(Guid gameId, CancellationToken cancellationToken)
    {
        return await context.GameStates.Where(
            gs => gs.GameId == gameId)
            .OrderBy(gs => gs.GenerationNumber)
            .LastOrDefaultAsync(cancellationToken);
    }
}
