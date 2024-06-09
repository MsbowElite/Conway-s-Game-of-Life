using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Application.Games;
using GameOfLife.Domain.GameStates;
using GameOfLife.SharedKernel;
using Mapster;

namespace GameOfLife.Application.GameStates.GetById;

public sealed record GetGameStateByIdQuery(Guid GameStateId) : IQuery<GameStateResponse> { }

internal sealed class GetGameByIdQueryHandler(
    IGameStateRepository gameStateRepository)
    : IQueryHandler<GetGameStateByIdQuery, GameStateResponse>
{
    public async Task<Result<GameStateResponse>> Handle(
        GetGameStateByIdQuery query,
        CancellationToken cancellationToken)
    {
        var gameState = await gameStateRepository.GetByIdAsync(query.GameStateId, cancellationToken);
        if (gameState == null) return Result.Failure<GameStateResponse>(GameErrors.NotFound(query.GameStateId));

        return gameState.Adapt<GameStateResponse>();
    }
}
