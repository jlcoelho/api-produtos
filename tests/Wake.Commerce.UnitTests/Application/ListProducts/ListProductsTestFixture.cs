

using Wake.Commerce.Application.UseCases.ListProducts;
using Wake.Commerce.Domain.Entity;
using Wake.Commerce.Domain.SeedWork.SearchableRepository;
using Wake.Commerce.UnitTests.Application.Common;
using Xunit;

namespace Wake.Commerce.UnitTests.Application.ListProducts;

[CollectionDefinition(nameof(ListProductsTestFixture))]
public class ListListProductsTestFixtureCollection
    : ICollectionFixture<ListProductsTestFixture>
{ }

public class ListProductsTestFixture
    : ProductUseCasesBaseFixture
{
    public List<Product> GetExampleProductsList(int length = 10)
    {
        var list = new List<Product>();
        for (int i = 0; i < length; i++)
            list.Add(GetExampleProduct());
        return list;
    }

    public ListProductsInput GetExampleInput()
    {
        var random = new Random();
        return new ListProductsInput(
            page: random.Next(1, 10),
            perPage: random.Next(15, 100),
            search: Faker.Commerce.ProductName(),
            sort: Faker.Commerce.ProductName(),
            dir: random.Next(0, 10) > 5 ?
                SearchOrder.Asc : SearchOrder.Desc
        );
    }
}
