using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Domain.Games;
using GameOfLife.Domain.GameStates;
using GameOfLife.SharedKernel;
using Mapster;

namespace GameOfLife.Application.Games.GetStateByGeneration;

public sealed record GetGameStateByGenerationQuery(Guid GameId, ushort GenerationNumber) : IQuery<GameStateResponse> { }

internal sealed class GetGameStateByGenerationQueryHandler(
    IGameRepository gameRepository,
    IGameStateRepository gameStateRepository)
    : IQueryHandler<GetGameStateByGenerationQuery, GameStateResponse>
{
    public async Task<Result<GameStateResponse>> Handle(
        GetGameStateByGenerationQuery query,
        CancellationToken cancellationToken)
    {
        var gameExists = await gameRepository.AnyByIdAsync(query.GameId, cancellationToken);
        if (!gameExists)
            return Result.Failure<GameStateResponse>(GameErrors.NotFound(query.GameId));

        var gameState = await gameStateRepository.GetByGameIdAndGenerationNumberAsync(
            query.GameId, query.GenerationNumber, cancellationToken);
        if (gameState == null)
            return Result.Failure<GameStateResponse>(GameErrors.StateNotFound(query.GenerationNumber));

        return gameState.Adapt<GameStateResponse>();
    }
}
