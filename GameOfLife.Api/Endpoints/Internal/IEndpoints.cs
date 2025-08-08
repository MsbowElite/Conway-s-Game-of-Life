namespace GameOfLife.Api.Endpoints.Internal;

internal interface IEndpoints
{
    abstract static void DefineEndpoints(IEndpointRouteBuilder app);
}
