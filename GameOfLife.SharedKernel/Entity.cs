namespace GameOfLife.SharedKernel
{
    public abstract class Entity
    {
        protected Entity(Guid id)
        {
            Id = id;
        }

        protected Entity()
        {
        }

        public Guid Id { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime LastUpdatedAt { get; private set; }
        public DateTime DeletedAt { get; private set; }
    }
}
