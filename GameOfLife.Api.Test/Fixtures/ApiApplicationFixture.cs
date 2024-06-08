using GameOfLife.Domain.Games;

namespace GameOfLife.Api.Test.Fixtures;

public class ApiApplicationFixture : IDisposable
{
    public ApiApplication Application;
    public Game Game;

    public ApiApplicationFixture()
    {
        Application = new ApiApplication();

        Game = new(
            Guid.NewGuid(),
            100,
            100

        );
    }

    public void Dispose()
    {
        Application.Dispose();
    }
}
