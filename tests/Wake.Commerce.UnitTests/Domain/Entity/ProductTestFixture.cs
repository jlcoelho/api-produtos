using Wake.Commerce.Domain.Entity;
using Wake.Commerce.UnitTests.Common;
using Xunit;

namespace Wake.Commerce.UnitTests.Domain.Entity;

public class ProductTestFixture : BaseFixture
{
    public ProductTestFixture() : base()
    {
    }

    public string GetValidProductName()
    {
        var productName = "";
        while(productName.Length < 4)
            productName = Faker.Commerce.ProductName();
        if (productName.Length > 255)
            productName = productName[..255];

        return productName;
    }

    public Product GetValidProduct() {
        return new Product(
            GetValidProductName(), 
            Faker.Random.Int(0, 10000), 
            Faker.Random.Decimal(0m, Faker.Random.Decimal(0, 100000))
        );
    }
}

[CollectionDefinition(nameof(ProductTestFixture))]
public class ProductTestFixtureCollection : ICollectionFixture<ProductTestFixture>
{ }