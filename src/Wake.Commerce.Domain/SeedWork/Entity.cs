namespace Wake.Commerce.Domain.SeedWork;

public abstract class Entity
{
    public Guid Id { get; protected set; }


    protected Entity()
    {
        this.Id = Guid.NewGuid();
    }
}
