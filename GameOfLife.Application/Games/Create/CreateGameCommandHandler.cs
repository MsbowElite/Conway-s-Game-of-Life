using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Domain.Games;
using GameOfLife.Domain.GameStates;
using GameOfLife.SharedKernel;
using GameOfLife.SharedKernel.Abstractions;

namespace GameOfLife.Application.Games.Create;

public sealed record CreateGameCommand(
    Guid? GameId,
    ushort Width,
    ushort Height,
    ushort[][] State) : ICommand<Guid>;

internal sealed class CreateGameCommandHandler(
    IGameRepository gameRepository,
    IGameStateRepository gameStateRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateGameCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
    CreateGameCommand command,
    CancellationToken cancellationToken)
    {
        var game = new Game(
            command.GameId is null ? Guid.NewGuid() : command.GameId.Value,
            command.Width,
            command.Height);

        var gameState = new GameState(
            Guid.NewGuid(),
            game.Id,
            command.State
            );

        await gameRepository.InsertAsync(game, cancellationToken);
        await gameStateRepository.InsertAsync(gameState, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return game.Id;
    }
}

