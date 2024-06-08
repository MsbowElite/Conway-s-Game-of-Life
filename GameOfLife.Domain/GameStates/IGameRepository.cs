namespace GameOfLife.Domain.GameStates;

public interface IGameStateRepository
{
    Task InsertAsync(GameState gameState, CancellationToken cancellationToken);
}
