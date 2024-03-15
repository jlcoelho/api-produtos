namespace Wake.Commerce.Domain.SeedWork;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }


    protected BaseEntity()
    {
        this.Id = Guid.NewGuid();
    }
}
