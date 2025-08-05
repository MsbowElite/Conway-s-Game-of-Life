using GameOfLife.Domain.Games;

namespace GameOfLife.Application.Test.Fixtures;

public static class GamesFixture
{
    public static Game GetGameMock() => new(
                new Guid(),
                100,
                100
            )
        ;
}
