using FluentValidation.Results;
using GameOfLife.Api.Endpoints.Internal;
using GameOfLife.Api.Infrastructure;
using GameOfLife.Application.Extensions;
using GameOfLife.Application.Games.Create;
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
    }

    internal static async Task<IResult> CreateGameAsync(
        CreateGameRequest request,
        ISender sender,
        CancellationToken cancellationToken)
    {
        byte[,] a = { { 0, 0 }, { 1, 2 }, { 2, 4 }, { 3, 6 }, { 4, 8 } };
        var command = request.Adapt<CreateGameCommand>();
        var result = await sender.Send(command, cancellationToken);

        return result.MatchCreated(
            BaseRoute,
            CustomResults.Problem);
    }
}
