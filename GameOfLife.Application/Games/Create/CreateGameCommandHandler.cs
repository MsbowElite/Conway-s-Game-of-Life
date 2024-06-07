using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Domain.Games;
using GameOfLife.Domain.GamesStates;
using GameOfLife.SharedKernel;
using GameOfLife.SharedKernel.Abstractions;

namespace GameOfLife.Application.Games.Create;

public sealed record CreateGameCommand(
    int Width,
    int Height,
    byte[,] State) : ICommand<Guid>;

internal sealed class CreateGameCommandHandler(
    IGameRepository gameRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateGameCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
    CreateGameCommand command,
    CancellationToken cancellationToken)
    {
        var game = new Game(
            Guid.NewGuid(),
            command.Width,
            command.Height);

        var gameState = new GamesState(
            Guid.NewGuid(),
            command.State
            );

        gameState.ExecuteNextGaneration();

        await gameRepository.InsertAsync(game, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return game.Id;
    }
}

