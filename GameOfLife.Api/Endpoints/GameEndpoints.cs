using FluentValidation.Results;
using GameOfLife.Api.Endpoints.Internal;
using GameOfLife.Api.Infrastructure;
using GameOfLife.Application.Extensions;
using GameOfLife.Application.Games;
using GameOfLife.Application.Games.Create;
using GameOfLife.Application.Games.GetById;
using Mapster;
using MediatR;

namespace GameOfLife.Api.Endpoints;

public class GameEndpoints : IEndpoints
{
    private const string ContentType = "application/json";
    private const string Tag = "Games";
    private const string BaseRoute = "games";
    private const string Slash = "/";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        var battles = app.MapGroup(BaseRoute)
            .WithTags(Tag);

        battles.MapPost(Slash, CreateGameAsync)
            .WithName("CreateGame")
            .Accepts<CreateGameRequest>(ContentType)
            .Produces<Guid>(201)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .WithOpenApi();

        battles.MapGet($"{Slash}{{id}}", GetGameByIdAsync)
            .WithName("GetGame")
            .Produces<GameResponse>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .Produces<string>(404)
            .WithOpenApi();
    }

    internal static async Task<IResult> CreateGameAsync(
        CreateGameRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateGameCommand>();
        var result = await sender.Send(command, cancellationToken);

        return result.MatchCreated(
            BaseRoute,
            CustomResults.Problem);
    }

    internal static async Task<IResult> GetGameByIdAsync(
        Guid gameId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetGameByIdQuery(gameId), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
