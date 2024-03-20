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

    public int GetValidProductStock()
    {
        return Faker.Random.Int(0, 1000);
    }

    public decimal GetValidProductPrice()
    {
        return Faker.Random.Decimal(0m, 10000);
    }

    public Product GetValidProduct() {
        return new Product(
            GetValidProductName(), 
            GetValidProductStock(), 
            GetValidProductPrice()
        );
    }
}

[CollectionDefinition(nameof(ProductTestFixture))]
public class ProductTestFixtureCollection : ICollectionFixture<ProductTestFixture>
{ }