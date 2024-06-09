using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Application.Games.ExecuteNextStateGeneration;
using GameOfLife.Domain.GameStates;
using GameOfLife.SharedKernel;
using Mapster;
using MediatR;

namespace GameOfLife.Application.Games.GetNextState;

public sealed record GetNextGameStateCommand(
    Guid GameId) : ICommand<object>;

internal sealed class GetNextGameStateCommandHandler(
    IGameStateRepository gameStateRepository,
    ISender sender)
    : ICommandHandler<GetNextGameStateCommand, object>
{
    public async Task<Result<object>> Handle(
    GetNextGameStateCommand command,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(new ExecuteNextGameStateGenerationCommand(command.GameId), cancellationToken);
        if (result.IsFailure)
            return result;

        var gameState = await gameStateRepository.GetByIdAsync(result.Value, cancellationToken);

        return gameState.Adapt<GameStateResponse>();
    }
}

