using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Application.Games.ExecuteNextStateGeneration;
using GameOfLife.Domain.GameStates;
using GameOfLife.SharedKernel;
using Mapster;
using MediatR;

namespace GameOfLife.Application.Games.GetSkipAndNextState;

public sealed record GetSkipAndNextGameStateCommand(
    Guid GameId, ushort Attempts) : ICommand<object>;

internal sealed class GetSkipAndNextGameStateCommandHandler(
    IGameStateRepository gameStateRepository,
    ISender sender)
    : ICommandHandler<GetSkipAndNextGameStateCommand, object>
{
    public async Task<Result<object>> Handle(
    GetSkipAndNextGameStateCommand command,
    CancellationToken cancellationToken)
    {
        var lastGameState = await gameStateRepository.GetLastByGameId(command.GameId, cancellationToken);
        if (lastGameState is null)
            return Result.Failure<Guid>(GameErrors.StateNotFound(command.GameId));

        Result<Guid> executeNextGameStateGenerationCommand = null;

        for (
            ushort counter = lastGameState.GenerationNumber;
            counter <= lastGameState.GenerationNumber + command.Attempts;
            counter++)
        {
            executeNextGameStateGenerationCommand = await sender.Send(new ExecuteNextGameStateGenerationCommand(command.GameId), cancellationToken);
            if (executeNextGameStateGenerationCommand.IsFailure)
                return executeNextGameStateGenerationCommand;
        }

        if(executeNextGameStateGenerationCommand is null)
            return Result.Failure(GameErrors.CriticalFailure());

        var gameState = await gameStateRepository.GetByIdAsync(executeNextGameStateGenerationCommand.Value, cancellationToken);

        return gameState.Adapt<GameStateResponse>();
    }
}

