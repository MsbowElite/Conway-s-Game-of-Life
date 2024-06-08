namespace GameOfLife.Application.Games;

public sealed record GameResponse
{
    public Guid Id { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastUpdatedAt { get; init; }
    public DateTime DeletedAt { get; init; }
}
