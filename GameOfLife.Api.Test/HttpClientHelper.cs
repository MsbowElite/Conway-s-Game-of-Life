using Newtonsoft.Json;

namespace GameOfLife.Api.Test;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "Tests must be public")]
public static class HttpClientHelper
{
    private static readonly JsonSerializer serializer = JsonSerializer.Create();

    public static async ValueTask<T?> ReadJsonResponser<T>(HttpResponseMessage response)
    {
        using Stream s = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        using var sr = new StreamReader(s);
        using JsonReader reader = new JsonTextReader(sr);
        return serializer.Deserialize<T>(reader);
    }
}
