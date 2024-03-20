using Wake.Commerce.Application.UseCases.CreateProduct;
using Wake.Commerce.Application.UseCases.UpdateProduct;
using Wake.Commerce.Domain.Entity;
using Wake.Commerce.Domain.SeedWork.SearchableRepository;
using Wake.Commerce.IntegrationTests.Application.UseCases.Common;
using Xunit;

namespace Wake.Commerce.IntegrationTests.Application.UseCases;

[CollectionDefinition(nameof(ProductTestUseCaseFixture))]
public class CreateProductTestFixtureCollection
    : ICollectionFixture<ProductTestUseCaseFixture>
{ }

public class ProductTestUseCaseFixture
    : ProductUseCasesBaseFixture
{
    public CreateProductInput GetInput()
    {
        var product = GetExampleProduct();
        return new CreateProductInput(
            product.Name, 
            product.Stock, 
            product.Price
        );
    }

    public UpdateProductInput GetValidInput(Guid? id = null)
        => new(
            id ?? Guid.NewGuid(),
            GetValidProductName(),
            GetRandomStock(),
            GetRandomPrice()
        );

        public List<Product> GetExampleProductsListWithNames(
        List<string> names
    ) => names.Select(name =>
    {
        var product = GetExampleProduct();
        product.Update(name);
        return product;
    }).ToList();

    public List<Product> CloneProductsListOrdered(
        List<Product> productsList,
        string orderBy,
        SearchOrder order
    )
    {
        var listClone = new List<Product>(productsList);
        var orderedEnumerable = (orderBy.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => listClone.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => listClone.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => listClone.OrderByDescending(x => x.CreatedAt),
            _ => listClone.OrderBy(x => x.Name).ThenBy(x => x.Id),
        };
        return orderedEnumerable.ToList();
    }
}
