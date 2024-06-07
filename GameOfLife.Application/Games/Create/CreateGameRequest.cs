namespace GameOfLife.Application.Games.Create;
/// <summary>
/// </summary>
/// <param name="Width"></param>
/// <param name="Height"></param>
/// <param name="State">
/// As ushort, we dont need to manage negative values and is more 
/// than enough to store the population size and smaller than int
/// </param>
public sealed record CreateGameRequest(
    int Width,
    int Height,
    ushort[][] State);
