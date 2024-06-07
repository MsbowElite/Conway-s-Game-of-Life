using GameOfLife.Domain.GameStates;
using GameOfLife.SharedKernel;
using System.Text.Json;

namespace GameOfLife.Domain.GamesStates
{
    public sealed class GamesState : Entity
    {
        public GamesState(Guid id, ushort[][] cellsState) : base(id) 
        {
            State = JsonSerializer.Serialize(cellsState);
        }

        private GamesState() { }

        public string State { get; set; }

        public void ExecuteNextGaneration()
        {
            var gameSimulation = new GameStateSimulation(this);
            State = gameSimulation.Next();
        }
    }
}
