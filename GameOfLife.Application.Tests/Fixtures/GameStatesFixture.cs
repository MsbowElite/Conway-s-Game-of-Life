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
                    [0,0,0,0,0,0],
                    [0,0,0,0,0,0],
                    [0,0,1,1,0,0],
                    [0,0,1,1,0,0],
                    [0,0,0,0,0,0],
                    [0,0,0,0,0,0]
                ]
            );
    }
}
