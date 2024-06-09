namespace GameOfLife.Application.Games;

public sealed record GameResponse
{
    public Guid Id { get; init; }
    public ushort Width { get; init; }
    public ushort Height { get; init; }
    public Guid? FinalGameStateId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime LastUpdatedAt { get; init; }
    public DateTime DeletedAt { get; init; }
}
