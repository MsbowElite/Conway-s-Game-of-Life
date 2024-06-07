namespace GameOfLife.Application.Games.Create;

public sealed record CreateGameRequest(
    int Width,
    int Height,
    byte[,] State);
