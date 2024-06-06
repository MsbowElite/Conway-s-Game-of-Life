using GameOfLife.SharedKernel;

namespace GameOfLife.Domain.GamesStates
{
    public sealed class GamesState : Entity
    {
        public GamesState(Guid id) : base(id) { }

        private GamesState() { }
    }
}
