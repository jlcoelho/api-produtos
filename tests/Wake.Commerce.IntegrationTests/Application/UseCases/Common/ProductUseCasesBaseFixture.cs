
using Wake.Commerce.Domain.Entity;
using Wake.Commerce.IntegrationTests.Base;

namespace Wake.Commerce.IntegrationTests.Application.UseCases.Common;
public class ProductUseCasesBaseFixture
    : BaseFixture
{
    public string GetValidProductName()
    {
        var productName = "";
        while (productName.Length < 3)
            productName = Faker.Commerce.Categories(1)[0];
        if (productName.Length > 255)
            productName = productName[..255];
        return productName;
    }

    public int GetRandomStock()
        => Faker.Random.Int(0, 10000);
    public decimal GetRandomPrice()
        => Faker.Random.Decimal(0, 10000);

    public Product GetExampleProduct()
        => new(
            GetValidProductName(),
            GetRandomStock(),
            GetRandomPrice()
        );

    public List<Product> GetExampleProductsList(int length = 10)
        => Enumerable.Range(1, length)
            .Select(_ => GetExampleProduct()).ToList();

}
