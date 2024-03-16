using Wake.Commerce.Domain.Entity;
using Xunit;

namespace Wake.Commerce.UnitTests.Domain.Entity;

public class ProductTestFixture
{
    public Product GetValidProduct() => new Product("Product Name", 10, 100);
}

[CollectionDefinition(nameof(ProductTestFixture))]
public class ProductTestFixtureCollection : ICollectionFixture<ProductTestFixture>
{}