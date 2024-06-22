using FluentValidation.Results;
using GameOfLife.Api.Test.Fixtures;
using GameOfLife.Application.Games;
using GameOfLife.Application.Games.Create;
using GameOfLife.SharedKernel;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Net;
using System.Net.Http.Json;

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
    public async Task A_0_PostCreateGame_GetCreatedStatusWithId()
    {
        var response = await _httpClient.PostAsJsonAsync("/games", _createGameRequest);

        var game = await HttpClientHelper.ReadJsonResponser<Guid>(response);
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

        var response = await _httpClient.PostAsJsonAsync("/games", request);

        var errorResult = await HttpClientHelper.ReadJsonResponser<Result>(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.IsType<Result>(errorResult);
    }

    [Fact]
    public async Task A_2_PostCreateGame_WithDuplicatedId_GetConflict()
    {
        var response = await _httpClient.PostAsJsonAsync("/games", _createGameRequest);

        var errorResult = await HttpClientHelper.ReadJsonResponser<Result>(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.IsType<Result>(errorResult);
    }

    [Fact]
    public async Task A_3_PostCreateGame_WithIdThatAlreadyExist_ReturnErrorConflict()
    {
        var response = await _httpClient.PostAsJsonAsync("/games", _createGameRequest);

        var errorResult = await HttpClientHelper.ReadJsonResponser<Result>(response);
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.IsType<Result>(errorResult);
    }

    [Fact]
    public async Task B_0_GetByIdReturnGame()
    {
        var response = await _httpClient.GetAsync($"/games/{_createGameRequest.GameId}");
        var game = await HttpClientHelper.ReadJsonResponser<GameResponse>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(_createGameRequest.GameId, game.Id);
    }

    [Fact]
    public async Task B_1_GetByIdThatNotExist_ReturnErrorNotFound()
    {
        var response = await _httpClient.GetAsync($"/games/{Guid.NewGuid()}");
        var errorResult = await HttpClientHelper.ReadJsonResponser<Result>(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.IsType<Result>(errorResult);
    }

    [Fact]
    public async Task C_0_ExecuteNextGaneration_ValidInput_ReturnIdOfNewGameState()
    {
        var response = await _httpClient.PostAsJsonAsync($"/games/{_createGameRequest.GameId}/GameStates/Next", string.Empty);

        var result = await HttpClientHelper.ReadJsonResponser<Guid>(response);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.IsType<Guid>(result);
    }

    [Fact]
    public async Task C_1_ExecuteNextGaneration_EmptyGameId_GetBadRequestValidationWithDescription()
    {
        var response = await _httpClient.PostAsJsonAsync($"/games/{Guid.Empty}/GameStates/Next", string.Empty);

        var result = await HttpClientHelper.ReadJsonResponser<Result>(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.IsType<Result>(result);
    }
}