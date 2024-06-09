using GameOfLife.Application.Games.Create;

namespace GameOfLife.Api.Test.Fixtures;

public class ApiApplicationFixture : IDisposable
{
    public ApiApplication Application;
    public CreateGameRequest CreateGameRequest;

    public ApiApplicationFixture()
    {
        Application = new ApiApplication();

        CreateGameRequest = new CreateGameRequest(
            Guid.NewGuid(),
            100,
            100,
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

    public void Dispose()
    {
        Application.Dispose();
    }
}
