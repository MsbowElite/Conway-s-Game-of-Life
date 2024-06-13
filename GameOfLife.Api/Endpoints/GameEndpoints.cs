using FluentValidation.Results;
using GameOfLife.Api.Endpoints.Internal;
using GameOfLife.Api.Infrastructure;
using GameOfLife.Application.Extensions;
using GameOfLife.Application.Games;
using GameOfLife.Application.Games.Create;
using GameOfLife.Application.Games.ExecuteNextStateGeneration;
using GameOfLife.Application.Games.GetById;
using GameOfLife.Application.Games.GetNextState;
using GameOfLife.Application.Games.GetSkipAndNextState;
using GameOfLife.Application.Games.GetStateByGeneration;
using GameOfLife.SharedKernel;
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
            .Produces<Result>(409)
            .Produces<Result>(500)
            .WithOpenApi();

        battles.MapPost($"{Slash}{{gameId}}{Slash}GameStates{Slash}Next", ExecuteNextGaneration)
            .WithName("ExecuteNextGaneration")
            .Produces<Guid>(201)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .Produces<Result>(404)
            .Produces<Result>(409)
            .Produces<Result>(500)
            .WithOpenApi();

        battles.MapGet($"{Slash}{{gameId}}{Slash}GameStates{Slash}Next", GetNextGaneration)
            .WithName("GetNextGaneration")
            .Produces<GameStateResponse>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .Produces<Result>(404)
            .Produces<Result>(409)
            .Produces<Result>(500)
            .WithOpenApi();

        battles.MapGet($"{Slash}{{gameId}}", GetGameByIdAsync)
            .WithName("GetGame")
            .Produces<GameResponse>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .Produces<Result>(404)
            .Produces<Result>(500)
            .WithOpenApi();

        battles.MapGet($"{Slash}{{gameId}}{Slash}GameStates{Slash}Final", GetFinalState)
            .WithName("GetFinalState")
            .Produces<GameStateResponse>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .Produces<Result>(404)
            .Produces<Result>(500)
            .WithOpenApi();

        battles.MapGet($"{Slash}{{gameId}}{Slash}GameStates{Slash}Next{Slash}{{attempts}}{Slash}away", SkipAttempsAndGetGameState)
            .WithName("SkipAttempsAndGetGameState")
            .Produces<GameStateResponse>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .Produces<Result>(404)
            .Produces<Result>(500)
            .WithOpenApi();

        battles.MapGet($"{Slash}{{gameId}}{Slash}GameStates{Slash}Generation{Slash}{{generationNumber}}", GetGameStateByGenerationAsync)
            .WithName("GetGameStateByGeneration")
            .Produces<GameResponse>(200)
            .Produces<IEnumerable<ValidationFailure>>(400)
            .Produces<Result>(404)
            .Produces<Result>(500)
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

    internal static async Task<IResult> ExecuteNextGaneration(
    Guid gameId,
    ISender sender,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(new ExecuteNextGameStateGenerationCommand(gameId), cancellationToken);

        return result.MatchCreated(
            GameStateEndpoints.BaseRoute,
            CustomResults.Problem);
    }

    internal static async Task<IResult> GetNextGaneration(
        Guid gameId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetNextGameStateCommand(gameId), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }

    internal static async Task<IResult> GetFinalState(
        Guid gameId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetNextGameStateCommand(gameId), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }

    internal static async Task<IResult> SkipAttempsAndGetGameState(
    Guid gameId,
    ushort attempts,
    ISender sender,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetSkipAndNextGameStateCommand(gameId, attempts), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }

    internal static async Task<IResult> GetGameByIdAsync(
        Guid gameId,
        ISender sender,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetGameByIdQuery(gameId), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }

    internal static async Task<IResult> GetGameStateByGenerationAsync(
    Guid gameId,
    ushort generationNumber,
    ISender sender,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetGameStateByGenerationQuery(gameId, generationNumber), cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
