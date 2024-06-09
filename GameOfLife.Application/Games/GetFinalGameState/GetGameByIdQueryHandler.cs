using GameOfLife.Application.Abstractions.Messaging;
using GameOfLife.Application.Games.ExecuteNextStateGeneration;
using GameOfLife.Application.Games.GetById;
using GameOfLife.Application.GameStates.GetById;
using GameOfLife.Domain.GameStates;
using GameOfLife.SharedKernel;
using Mapster;
using MediatR;
using System.Reflection;

namespace GameOfLife.Application.Games.GetFinalGameState;

public sealed record GetFinalGameStateQuery(Guid GameId) : IQuery<object> { }

internal sealed class GetGameByIdQueryHandler(
        IGameStateRepository gameRepository,
        ISender sender)
    : IQueryHandler<GetFinalGameStateQuery, object>
{
    public async Task<Result<object>> Handle(
        GetFinalGameStateQuery query,
        CancellationToken cancellationToken)
    {
        var getGameByIdQueryResult = await sender.Send(new GetGameByIdQuery(query.GameId), cancellationToken);
        if (getGameByIdQueryResult.IsFailure)
            return getGameByIdQueryResult;

        Guid finalGameStateId;

        if (getGameByIdQueryResult.Value.FinalGameStateId is not null)
        {
            finalGameStateId = getGameByIdQueryResult.Value.FinalGameStateId.Value;
        }
        else
        {
            Result<Guid> executeNextGameStateGenerationResult;
            Guid lastGameStateId = Guid.Empty;
            do
            {
                executeNextGameStateGenerationResult = await sender.Send(new ExecuteNextGameStateGenerationCommand(query.GameId));
                if (executeNextGameStateGenerationResult.IsSuccess)
                    lastGameStateId = executeNextGameStateGenerationResult.Value;

            } while (executeNextGameStateGenerationResult.IsSuccess);

            finalGameStateId = lastGameStateId;
        }

        return await sender.Send(new GetGameStateByIdQuery(finalGameStateId), cancellationToken);
    }
}
