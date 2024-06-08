using GameOfLife.Domain.GameStates;
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

    private Game() 
    {
        GameStates = [];
    }

    public ushort Width { get; }
    public ushort Height { get; }
    public Guid? FinalGameStateId { get; }

    public GameState? GameState { get; set; } = null;

    public ICollection<GameState> GameStates { get; set; }
}
