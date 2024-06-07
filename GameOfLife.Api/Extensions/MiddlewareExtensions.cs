using GameOfLife.Api.Middleware;

namespace GameOfLife.Api.Extensions;

public static class MiddlewareExtensions
{
    /// <summary>
    /// Apply an uniqueId for all logs of a scoped request to help tracking errors and logs.
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseRequestContextLogging(this IApplicationBuilder app)
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}
