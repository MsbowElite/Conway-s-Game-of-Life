namespace GameOfLife.Domain.GameStates;

public interface IGameStateRepository
{
    Task InsertAsync(GameState gameState, CancellationToken cancellationToken);
    Task<GameState> GetByIdAsync(Guid gameStateId, CancellationToken cancellationToken);
    Task<GameState> GetByGameIdAndGenerationNumberAsync(
        Guid gameId, ushort generationNumber, CancellationToken cancellationToken);
    Task<GameState> GetLastByGameId(Guid gameId, CancellationToken cancellationToken);
}
