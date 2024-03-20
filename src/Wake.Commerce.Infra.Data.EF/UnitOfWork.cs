using Wake.Commerce.Application.Interfaces;

namespace Wake.Commerce.Infra.Data.EF;

public class UnitOfWork : IUnitOfWork
{
    private readonly WakeCommerceDbContext _context;

    public UnitOfWork(WakeCommerceDbContext context)
    {
        _context = context;
    }
    public Task Commit(CancellationToken cancellationToken)
        => _context.SaveChangesAsync(cancellationToken);
    

    public Task Rollback(CancellationToken cancellationToken)
        => Task.CompletedTask;
}