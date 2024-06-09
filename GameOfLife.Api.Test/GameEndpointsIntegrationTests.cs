using GameOfLife.Api.Test.Fixtures;
using GameOfLife.Application.Games;
using GameOfLife.Application.Games.Create;
using GameOfLife.Domain.Games;
using System.Net;
using System.Net.Http.Json;
using System.Threading;

namespace GameOfLife.Api.Test;

/// <summary>
/// Execute only in sequence.
/// </summary>
[TestCaseOrderer("GameOfLife.Api.Test.AlphabeticalOrderer", "GameOfLife.Api.Test")]
public class GameEndpointsIntegrationTests : IClassFixture<ApiApplicationFixture>
{
    private readonly HttpClient _httpClient;
    private readonly CreateGameRequest _createGameRequest;

    public GameEndpointsIntegrationTests(ApiApplicationFixture apiApplicationFixture)
    {
        _httpClient = apiApplicationFixture.Application.CreateClient();
        _createGameRequest = apiApplicationFixture.CreateGameRequest;
    }

    [Fact]
    public async Task A_PostCreateGame_GetCreatedStatusWithId()
    {
        var response = await _httpClient.PostAsJsonAsync("/games", _createGameRequest);
        
        var game = await HttpClientHelper.ReadJsonResponser<Guid>(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<Guid>(game);
    }

    [Fact]
    public async Task B_GetByIdReturnGame()
    {
        var response = await _httpClient.GetAsync($"/games/{_createGameRequest.GameId}");
        var game = await HttpClientHelper.ReadJsonResponser<GameResponse>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(_createGameRequest.GameId, game.Id);
    }
}