using GameOfLife.Domain.Games;
using GameOfLife.Infrastructure.Database;

namespace GameOfLife.Infrastructure.Repositories;

public sealed class GameRepository(GameContext context) : IGameRepository
{
    public Task InsertAsync(Game game, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
