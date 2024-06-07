using GameOfLife.Domain.Games;
using GameOfLife.Infrastructure.Database;

namespace GameOfLife.Infrastructure.Repositories;

internal sealed class GameRepository(GameContext context) : IGameRepository
{
    public Task InsertAsync(Game game, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
