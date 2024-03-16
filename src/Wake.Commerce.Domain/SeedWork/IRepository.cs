namespace Wake.Commerce.Domain.SeedWork;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    public Task Insert(TEntity entity, CancellationToken cancellationToken);
    public Task<TEntity> Get(Guid id, CancellationToken cancellationToken);
    public Task Delete(TEntity entity, CancellationToken cancellationToken);
    public Task Update(TEntity entity, CancellationToken cancellationToken);
}
