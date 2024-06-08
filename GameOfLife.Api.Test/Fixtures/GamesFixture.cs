using GameOfLife.Domain.Games;

namespace GameOfLife.Api.Test.Fixtures;

public static class GamesFixture
{
    public static IEnumerable<Game> GetBattlesMock()
    {
        var MonsterIdA = Guid.NewGuid();
        return
        [
            new Game(
                Guid.NewGuid(),
                100,
                100
            )
        ];
    }
}
