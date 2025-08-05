namespace GameOfLife.Api.Endpoints.Internal;

public interface IEndpoints
{
    abstract static void DefineEndpoints(IEndpointRouteBuilder app);
}
