using FluentValidation.Results;
using GameOfLife.Api.Endpoints.Internal;
using GameOfLife.Api.Infrastructure;
using GameOfLife.Application.Extensions;
using GameOfLife.Application.Games;
using GameOfLife.Application.GameStates.GetById;
using GameOfLife.SharedKernel;
using MediatR;

namespace GameOfLife.Api.Endpoints;

public class GameStateEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "GameStates";
    public const string BaseRoute = "gamestates";
    private const string Slash = "/";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        var battleStates = app.MapGroup(BaseRoute)
            .WithTags(Tag);

        battleStates.MapGet($"{Slash}{{gameStateId}}", GetGameStateByIdAsync)
            .WithName("GetGameState")
            .Produces<GameStateResponse>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .Produces<Result>(404)
            .WithOpenApi();
    }

    internal static async Task<IResult> GetGameStateByIdAsync(
    Guid gameStateId,
    ISender sender,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetGameStateByIdQuery(gameStateId), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}