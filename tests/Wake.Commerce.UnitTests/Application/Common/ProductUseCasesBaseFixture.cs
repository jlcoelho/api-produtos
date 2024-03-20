using Moq;
using Wake.Commerce.Application.Interfaces;
using Wake.Commerce.Domain.Entity;
using Wake.Commerce.Domain.Repository;
using Wake.Commerce.UnitTests.Common;

namespace Wake.Commerce.UnitTests.Application.Common;

public abstract class ProductUseCasesBaseFixture
    : BaseFixture
{

    public Mock<IProductRepository> GetRepositoryMock()
        => new();

    public Mock<IUnitOfWork> GetUnitOfWorkMock()
        => new();

    public string GetValidProductName()
    {
        var productName = "";
        while (productName.Length < 4)
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

    public Product GetExampleProduct()
        => new Product(
            GetValidProductName(),
            GetValidProductStock(),
            GetValidProductPrice()
        );
}
