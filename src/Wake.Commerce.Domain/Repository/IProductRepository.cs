using Wake.Commerce.Domain.Entity;
using Wake.Commerce.Domain.SeedWork;
using Wake.Commerce.Domain.SeedWork.SearchableRepository;

namespace Wake.Commerce.Domain.Repository;
public interface IProductRepository: IRepository<Product>,ISearchableRepository<Product>
{
}
