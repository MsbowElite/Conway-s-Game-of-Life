using GameOfLife.Domain.Games;

namespace GameOfLife.Application.Test.Fixtures;

public static class GamesFixture
{
    public static Game GetGameMock()
    {
        return
            new Game(
                new Guid(),
                100,
                100
            )
        ;
    }
}
