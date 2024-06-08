using GameOfLife.SharedKernel;

namespace GameOfLife.Application.Extensions;

public static class ResultExtensions
{
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }

    /// <summary>
    /// Adding a header with the location of get of created object
    /// and the id in the body following http status 201
    /// </summary>
    /// <typeparam name="TIn"></typeparam>
    /// <param name="result"></param>
    /// <param name="baseAddress"></param>
    /// <param name="onFailure"></param>
    /// <returns></returns>
    public static IResult MatchCreated<TIn>(
    this Result<TIn> result,
    string baseAddress,
    Func<Result<TIn>, IResult> onFailure)
    {
        return result.IsSuccess ? Results.Created($"/{baseAddress}/{result.Value}", result.Value) : onFailure(result);
    }
}
