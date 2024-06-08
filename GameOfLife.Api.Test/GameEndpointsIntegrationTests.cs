using GameOfLife.Api.Test.Fixtures;
using GameOfLife.Application.Games;
using GameOfLife.Application.Games.Create;
using GameOfLife.Domain.Games;
using System.Net;
using System.Net.Http.Json;
using System.Threading;

namespace GameOfLife.Api.Test;

[TestCaseOrderer("API.Test.AlphabeticalOrderer", "GameOfLife.Api.Test")]
public class GameEndpointsIntegrationTests : IClassFixture<ApiApplicationFixture>
{
    private readonly ApiApplication _application;
    private readonly Game _game;

    public GameEndpointsIntegrationTests(ApiApplicationFixture apiApplicationFixture)
    {
        _application = apiApplicationFixture.Application;
        _game = apiApplicationFixture.Game;
    }

    [Fact]
    public async Task A_PostCreateGame_GetCreatedStatusWithId()
    {
        using var client = _application.CreateClient();
        var request = new CreateGameRequest(
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

        var response = await client.PostAsJsonAsync("/games", request);
        
        var gameResponse = await HttpClientHelper.ReadJsonResponser<Guid>(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<Guid>(gameResponse);
    }

    [Fact]
    public async Task B_GetByIdReturnGame()
    {
        using var client = _application.CreateClient();

        var response = await client.GetAsync("/games");
        var game = await HttpClientHelper.ReadJsonResponser<GameResponse>(response);
        Assert.Equal(_game.Id, game.Id);
    }
}