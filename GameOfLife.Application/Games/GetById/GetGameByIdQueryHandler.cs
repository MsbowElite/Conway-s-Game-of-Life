using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Domain.Games;
using GameOfLife.SharedKernel;
using Mapster;

namespace GameOfLife.Application.Games.GetById;

public sealed record GetGameByIdQuery(Guid GameId) : IQuery<GameResponse> { }

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
