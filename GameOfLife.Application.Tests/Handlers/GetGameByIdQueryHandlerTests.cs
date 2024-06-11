using GameOfLife.Application.Games;
using GameOfLife.Application.Games.Create;
using GameOfLife.Application.Games.GetById;
using GameOfLife.Application.Test.Fixtures;
using GameOfLife.Application.Tests.Repositories;
using GameOfLife.Domain.Games;
using GameOfLife.Domain.GameStates;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Application.Tests.Handlers;

public class GetGameByIdQueryHandlerTests
{
    private readonly IGameRepository _gameRepository;

    public GetGameByIdQueryHandlerTests()
    {
        _gameRepository = new FakeGameRepository();
    }

    [Fact]
    public async Task If_TryToGetExistingGame_ReturnGame()
    {
        var getGameByIdQuery = new GetGameByIdQuery(GamesFixture.GetGameMock().Id);
        var getGameByIdQueryHandler = new GetGameByIdQueryHandler(_gameRepository);

        var result = await getGameByIdQueryHandler.Handle(getGameByIdQuery, default);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.Id, GamesFixture.GetGameMock().Id);
    }

    [Fact]
    public async Task If_TryToGetNoExistingGame_ReturnFailure_NotFound()
    {
        var id = Guid.NewGuid();
        var getGameByIdQuery = new GetGameByIdQuery(id);
        var getGameByIdQueryHandler = new GetGameByIdQueryHandler(_gameRepository);

        var result = await getGameByIdQueryHandler.Handle(getGameByIdQuery, default);

        Assert.NotNull(result);
        Assert.True(result.IsFailure);
        Assert.Equal(result.Error, GameErrors.NotFound(id));
    }
}
