using GameOfLife.Domain.Games;
using GameOfLife.SharedKernel;
using System.Text.Json;

namespace GameOfLife.Domain.GameStates;

public sealed class GameState : Entity
{
    public GameState(
        Guid id,
        Guid gameId,
        ushort[][] cellsState) : base(id)
    {
        GameId = gameId;
        State = JsonSerializer.Serialize(cellsState);
    }

    private GameState() { }

    public string State { get; set; }
    public ushort GenerationNumber { get; set; }

    public Guid GameId { get; set; }
    public Game? GameRelation { get; set; }

    public void ExecuteNextGaneration()
    {
        var gameSimulation = new GameStateSimulation(this);
        State = gameSimulation.Next();
    }
}
