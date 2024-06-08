using GameOfLife.Domain.GameStates;

namespace GameOfLife.Api.Test.Fixtures;

public static class GameStatesFixture
{
    public static IEnumerable<GameState> GetBattlesMock()
    {
        var MonsterIdA = Guid.NewGuid();
        return
        [
            new GameState(
                Guid.NewGuid(),
                Guid.NewGuid(),
                [
                    [0,0,0,0,0,0],
                    [0,0,0,0,0,0],
                    [0,0,1,1,0,0],
                    [0,0,1,1,0,0],
                    [0,0,0,0,0,0],
                    [0,0,0,0,0,0]
                ]
            )
        ];
    }
}
