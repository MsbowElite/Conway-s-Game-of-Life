using GameOfLife.SharedKernel;

namespace GameOfLife.Domain.Games;

public sealed class Game : Entity
{
    public Game(
        Guid id,
        ushort width,
        ushort height) : base(id)
    {
        Width = width;
        Height = height;
    }

    private Game() { }

    public ushort Width { get; }
    public ushort Height { get; }
    public ushort FinalGenerationNumber { get; }
}
