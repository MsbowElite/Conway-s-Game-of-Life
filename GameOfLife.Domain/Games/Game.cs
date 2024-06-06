using GameOfLife.SharedKernel;

namespace GameOfLife.Domain.Games
{
    public sealed class Game : Entity
    {
        public Game(Guid id) : base(id) { }

        private Game() { }
    }
}
