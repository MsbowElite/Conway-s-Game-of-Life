using GameOfLife.Application.Test.Fixtures;
using GameOfLife.Domain.Games;

namespace GameOfLife.Application.Tests.Repositories;

public class FakeGameRepository : IGameRepository
{
    public async Task<bool> AnyByIdAsync(Guid gameId, CancellationToken cancellationToken)
    {
        if (gameId == GamesFixture.GetGameMock().Id)
            return true;
        return false;
    }

    public async Task<Game> GetByIdAsync(Guid gameId, CancellationToken cancellationToken)
    {
        if (gameId == GamesFixture.GetGameMock().Id)
            return GamesFixture.GetGameMock();
        return null;
    }

    public async Task InsertAsync(Game game, CancellationToken cancellationToken)
    {
    }
}
