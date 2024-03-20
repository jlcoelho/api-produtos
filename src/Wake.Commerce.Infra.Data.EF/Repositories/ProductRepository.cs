using Microsoft.EntityFrameworkCore;
using Wake.Commerce.Application.Exceptions;
using Wake.Commerce.Domain.Entity;
using Wake.Commerce.Domain.Repository;
using Wake.Commerce.Domain.SeedWork.SearchableRepository;

namespace Wake.Commerce.Infra.Data.EF.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly WakeCommerceDbContext _context;
    private DbSet<Product> _products => _context.Set<Product>();

    public ProductRepository(WakeCommerceDbContext context)
    {
        _context = context;
    }

    public async Task Insert(Product entity, CancellationToken cancellationToken)
    {
        await _products.AddAsync(entity, cancellationToken);
    }

    public async Task Update(Product entity, CancellationToken cancellationToken)
    {
        await Task.FromResult(_products.Update(entity));   
    }

    public async Task Delete(Product entity, CancellationToken cancellationToken)
    {
        await Task.FromResult(_products.Remove(entity));   
    }

    public async Task<Product> Get(Guid id, CancellationToken cancellationToken)
    {
        var product = await _products.AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken
        );
        NotFoundException.ThrowIfNull(product, $"Product '{id}' not found.");
        return product!;
    }

    public async Task<SearchOutput<Product>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = _products.AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        if(!String.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Name.Contains(input.Search));
        var total = await query.CountAsync();
        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();
        return new(input.Page, input.PerPage, total, items);
    }

        private IQueryable<Product> AddOrderToQuery(
        IQueryable<Product> query,
        string orderProperty,
        SearchOrder order
    )
    { 
        var orderedQuery = (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
        };
        return orderedQuery;
    }
}