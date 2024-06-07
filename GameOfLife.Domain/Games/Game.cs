using GameOfLife.SharedKernel;

namespace GameOfLife.Domain.Games;

public sealed class Game : Entity
{
    public Game(
        Guid id, 
        int width, 
        int height) : base(id) 
    {
        Width = width;
        Height = height;
    }

    private Game() { }

    public int Width { get; }
    public int Height { get; }
}
