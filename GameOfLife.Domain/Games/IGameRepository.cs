namespace GameOfLife.Domain.Games;

public interface IGameRepository
{
    Task InsertAsync(Game game, CancellationToken cancellationToken);
    Task<Game> GetByIdAsync(Guid gameId, CancellationToken cancellationToken);
}
