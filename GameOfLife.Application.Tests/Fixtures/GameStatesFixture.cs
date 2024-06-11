using GameOfLife.Domain.GameStates;

namespace GameOfLife.Application.Test.Fixtures;

public static class GameStatesFixture
{
    public static GameState GetGameState()
    {
        return
            new GameState(
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
}
