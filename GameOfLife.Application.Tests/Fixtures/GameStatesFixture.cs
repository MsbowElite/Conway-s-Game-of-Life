using GameOfLife.Domain.GameStates;

namespace GameOfLife.Application.Test.Fixtures;

internal static class GameStatesFixture
{
    public static GameState GetGameState() => new(
                Guid.NewGuid(),
                GamesFixture.GetGameMock().Id,
                [
                    [false,false,false,false,false,false],
                    [false,false,false,false,false,false],
                    [false,true,true,true,true,false],
                    [false,false,false,false,false,false],
                    [false,false,false,false,false,false],
                    [false,false,false,false,false,false]
                ]
            );
}
