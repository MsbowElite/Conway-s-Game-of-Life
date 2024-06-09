using GameOfLife.Application.Abstractions.Caching;
using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Domain.Games;
using GameOfLife.SharedKernel;
using Mapster;

namespace GameOfLife.Application.Games.GetById;

/// <summary>
/// Using caching for game query
/// We must invalidate the cache when change DeletedAt or when fill the FinalGameStateId.
/// Applying cache invalidation we can increase the cache time and make the information more reliable.
/// </summary>
/// <param name="GameId"></param>
public sealed record GetGameByIdQuery(Guid GameId) : ICachedQuery<GameResponse> 
{
    public string CacheKey => $"game-by-id-{GameId}";

    public TimeSpan? Expiration => TimeSpan.FromMinutes(5);
}

internal sealed class GetGameByIdQueryHandler(
    IGameRepository gameRepository)
    : IQueryHandler<GetGameByIdQuery, GameResponse>
{
    public async Task<Result<GameResponse>> Handle(
        GetGameByIdQuery query,
        CancellationToken cancellationToken)
    {
        var game = await gameRepository.GetByIdAsync(query.GameId, cancellationToken);
        if (game == null) return Result.Failure<GameResponse>(GameErrors.NotFound(query.GameId));

        return game.Adapt<GameResponse>();
    }
}
