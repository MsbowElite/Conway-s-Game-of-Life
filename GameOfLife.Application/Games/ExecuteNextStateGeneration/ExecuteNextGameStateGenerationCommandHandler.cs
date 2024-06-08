using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Domain.Games;
using GameOfLife.Domain.GameStates;
using GameOfLife.SharedKernel;
using GameOfLife.SharedKernel.Abstractions;

namespace GameOfLife.Application.Games.ExecuteNextStateGeneration;

public sealed record ExecuteNextGameStateGenerationCommand(
    Guid GameId) : ICommand<Guid>;

internal sealed class ExecuteNextGameStateGenerationCommandHandler(
    IGameRepository gameRepository,
    IGameStateRepository gameStateRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<ExecuteNextGameStateGenerationCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
    ExecuteNextGameStateGenerationCommand command,
    CancellationToken cancellationToken)
    {
        var gameExists = await gameRepository.AnyByIdAsync(command.GameId, cancellationToken);
        if (!gameExists)
            return Result.Failure<Guid>(GameErrors.NotFound(command.GameId));

        var lastGameState = await gameStateRepository.GetLastByGameId(command.GameId, cancellationToken);
        if (lastGameState == null)
            return Result.Failure<Guid>(GameErrors.StateNotFound(command.GameId));

        var newGameState = new GameState(
            Guid.NewGuid(),
            command.GameId,
            lastGameState.State,
            lastGameState.GenerationNumber
            );
        newGameState.ExecuteNextGaneration();

        await gameStateRepository.InsertAsync(newGameState, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return newGameState.Id;
    }
}

