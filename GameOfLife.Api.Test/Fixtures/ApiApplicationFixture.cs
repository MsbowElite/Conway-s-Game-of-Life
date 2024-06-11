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
                [false,false,false,false,false,false],
                [false,false,false,false,false,false],
                [false,true,true,true,true,false],
                [false,false,false,false,false,false],
                [false,false,false,false,false,false],
                [false,false,false,false,false,false]
            ]
        );
    }

    public void Dispose()
    {
        Application.Dispose();
    }
}
