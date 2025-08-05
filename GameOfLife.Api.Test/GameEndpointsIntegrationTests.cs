using GameOfLife.Api.Test.Fixtures;
using GameOfLife.Application.Games;
using GameOfLife.Application.Games.Create;
using GameOfLife.SharedKernel;
using System.Net;
using System.Net.Http.Json;

namespace GameOfLife.Api.Test;

/// <summary>
/// Execute only in sequence.
/// </summary>
[TestCaseOrderer("GameOfLife.Api.Test.AlphabeticalOrderer", "GameOfLife.Api.Test")]
public class GameEndpointsIntegrationTests(ApiApplicationFixture apiApplicationFixture) : IClassFixture<ApiApplicationFixture>
{
    private readonly HttpClient _httpClient = apiApplicationFixture.Application.CreateClient();
    private readonly CreateGameRequest _createGameRequest = apiApplicationFixture.CreateGameRequest;

    [Fact]
    public async Task A_0_PostCreateGame_GetCreatedStatusWithId()
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/games", _createGameRequest);

        Guid game = await HttpClientHelper.ReadJsonResponser<Guid>(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<Guid>(game);
    }

    [Fact]
    public async Task A_1_PostCreateGame_WithZeroWidthHeight_GetBadRequestValidationWithDescription()
    {
        var request = new CreateGameRequest(
            _createGameRequest.GameId,
            0,
            0,
            _createGameRequest.State
            );

        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/games", request);

        Result errorResult = await HttpClientHelper.ReadJsonResponser<Result>(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.IsType<Result>(errorResult);
    }

    [Fact]
    public async Task A_2_PostCreateGame_WithDuplicatedId_GetConflict()
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/games", _createGameRequest);

        Result errorResult = await HttpClientHelper.ReadJsonResponser<Result>(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.IsType<Result>(errorResult);
    }

    [Fact]
    public async Task A_3_PostCreateGame_WithIdThatAlreadyExist_ReturnErrorConflict()
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/games", _createGameRequest);

        Result errorResult = await HttpClientHelper.ReadJsonResponser<Result>(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.IsType<Result>(errorResult);
    }

    [Fact]
    public async Task B_0_GetByIdReturnGame()
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/games/{_createGameRequest.GameId}");
        GameResponse game = await HttpClientHelper.ReadJsonResponser<GameResponse>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(_createGameRequest.GameId, game.Id);
    }

    [Fact]
    public async Task B_1_GetByIdThatNotExist_ReturnErrorNotFound()
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"/games/{Guid.NewGuid()}");
        Result errorResult = await HttpClientHelper.ReadJsonResponser<Result>(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.IsType<Result>(errorResult);
    }

    [Fact]
    public async Task C_0_ExecuteNextGaneration_ValidInput_ReturnIdOfNewGameState()
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"/games/{_createGameRequest.GameId}/GameStates/Next", string.Empty);

        Guid result = await HttpClientHelper.ReadJsonResponser<Guid>(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<Guid>(result);
    }

    [Fact]
    public async Task C_1_ExecuteNextGaneration_EmptyGameId_GetBadRequestValidationWithDescription()
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"/games/{Guid.Empty}/GameStates/Next", string.Empty);

        Result result = await HttpClientHelper.ReadJsonResponser<Result>(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.IsType<Result>(result);
    }
}
