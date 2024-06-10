using GameOfLife.Application.Games;
using GameOfLife.Application.Games.ExecuteNextStateGeneration;
using GameOfLife.Application.Games.GetNextState;
using GameOfLife.Application.Test.Fixtures;
using GameOfLife.Domain.GameStates;
using GameOfLife.SharedKernel;
using MediatR;
using Moq;

namespace GameOfLife.Application.Tests.Handlers;

public class GetNextGameStateCommandHandlerTests
{
    private readonly Mock<IGameStateRepository> _gameStateRepository;
    private readonly Mock<ISender> _sender;

    public GetNextGameStateCommandHandlerTests()
    {
        _gameStateRepository = new Mock<IGameStateRepository>();
        _sender = new Mock<ISender>();
    }

    [Fact]
    public async Task RequestValidNextGameState_ReturnState()
    {
        _sender.Setup(s => s.Send(new ExecuteNextGameStateGenerationCommand(It.IsAny<Guid>()), default))
                .Returns(Task.FromResult(Result.Success(GameStatesFixture.GetGameState().Id)));

        _gameStateRepository.Setup(s => s.GetByIdAsync(It.IsAny<Guid>(), default))
            .Returns(Task.FromResult(GameStatesFixture.GetGameState()));

        var getNextGameStateCommandHandler = new GetNextGameStateCommandHandler(_gameStateRepository.Object, _sender.Object);
        var result = await getNextGameStateCommandHandler.Handle(
            new GetNextGameStateCommand(GamesFixture.GetGameMock().Id),
            default);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.IsAssignableFrom<GameStateResponse>(result.Value);
    }
}
