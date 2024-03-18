using Wake.Commerce.Application.UseCases.CreateProduct;
using Wake.Commerce.UnitTests.Application.Common;
using Xunit;

namespace Wake.Commerce.UnitTests.Application.CreateProduct;

[CollectionDefinition(nameof(CreateProductTestFixture))]
public class CreateProductTestFixtureCollection 
    : ICollectionFixture<CreateProductTestFixture>
{}


public class CreateProductTestFixture : ProductUseCasesBaseFixture
{
    public CreateProductInput GetInput()
        => new(
            GetValidProductName(),
            GetValidProductStock(),
            GetValidProductPrice()
        );

    public CreateProductInput GetInvalidInputShortName()
    {
        var productInput = GetInput();
        productInput.Name =
            productInput.Name.Substring(0, 2);
        return productInput;
    }

    public CreateProductInput GetInvalidInputTooLongName()
    {
        var productInput = GetInput();
        var tooLongNameForProduct = Faker.Commerce.ProductName();
        while (tooLongNameForProduct.Length <= 255)
            tooLongNameForProduct = $"{tooLongNameForProduct} {Faker.Commerce.ProductName()}";
        productInput.Name = tooLongNameForProduct;
        return productInput;
    }

    public CreateProductInput GetInvalidInputStockLessThanZero()
    {
        var productInput = GetInput();
        productInput.Stock = -2;
        return productInput;      
    }

    public CreateProductInput GetInvalidInputPriceLessThanZero()
    {
        var productInput = GetInput();
        productInput.Price = -2.5m;
        return productInput;      
    }
}