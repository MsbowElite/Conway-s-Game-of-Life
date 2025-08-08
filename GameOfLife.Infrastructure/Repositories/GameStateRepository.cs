using GameOfLife.Domain.GameStates;
using GameOfLife.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace GameOfLife.Infrastructure.Repositories;

public sealed class GameStateRepository(GameContext context) : IGameStateRepository
{
    public async Task InsertAsync(GameState gameState, CancellationToken cancellationToken) => 
        await context.GameStates.AddAsync(gameState, cancellationToken);

    public async ValueTask<GameState?> GetByIdAsync(Guid gameStateId, CancellationToken cancellationToken)
        => await context.GameStates.FindAsync(new object[] { gameStateId }, cancellationToken);

    public async ValueTask<GameState?> GetByGameIdAndGenerationNumberAsync(
        Guid gameId, ushort generationNumber, CancellationToken cancellationToken) => await context.GameStates.Where(
            gs => gs.GameId == gameId && gs.GenerationNumber == generationNumber)
            .FirstOrDefaultAsync(cancellationToken);

    public async ValueTask<GameState?> GetLastByGameId(Guid gameId, CancellationToken cancellationToken) => await context.GameStates.Where(
            gs => gs.GameId == gameId)
            .OrderBy(gs => gs.GenerationNumber)
            .LastOrDefaultAsync(cancellationToken);
}
