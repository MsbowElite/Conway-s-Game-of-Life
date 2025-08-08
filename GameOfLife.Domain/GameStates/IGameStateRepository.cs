namespace GameOfLife.Domain.GameStates;

public interface IGameStateRepository
{
    Task InsertAsync(GameState gameState, CancellationToken cancellationToken);
    ValueTask<GameState?> GetByIdAsync(Guid gameStateId, CancellationToken cancellationToken);
    ValueTask<GameState?> GetByGameIdAndGenerationNumberAsync(
        Guid gameId, ushort generationNumber, CancellationToken cancellationToken);
    ValueTask<GameState?> GetLastByGameId(Guid gameId, CancellationToken cancellationToken);
}
