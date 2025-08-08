using GameOfLife.Application.Games.Create;

namespace GameOfLife.Api.Test.Fixtures;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "Tests must be public")]
public sealed class ApiApplicationFixture : IDisposable
{
    public ApiApplication Application { get; }
    public CreateGameRequest CreateGameRequest { get; }

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

    public void Dispose() => Application.Dispose();
}
