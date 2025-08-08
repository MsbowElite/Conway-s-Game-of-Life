using GameOfLife.Application.Test.Fixtures;
using GameOfLife.Domain.Games;

namespace GameOfLife.Application.Tests.Repositories;

internal sealed class FakeGameRepository : IGameRepository
{
    public Task<bool> AnyByIdAsync(Guid gameId, CancellationToken cancellationToken)
    {
        if (gameId == GamesFixture.GetGameMock().Id)
            return Task.FromResult(true);
        return Task.FromResult(false);
    }

    public ValueTask<Game?> GetByIdAsync(Guid gameId, CancellationToken cancellationToken)
    {
        if (gameId == GamesFixture.GetGameMock().Id)
            return new ValueTask<Game?>(Task.FromResult<Game?>(GamesFixture.GetGameMock()));
        return new ValueTask<Game?>(Task.FromResult<Game?>(null));
    }

    public Task InsertAsync(Game game, CancellationToken cancellationToken) => Task.CompletedTask;
}
