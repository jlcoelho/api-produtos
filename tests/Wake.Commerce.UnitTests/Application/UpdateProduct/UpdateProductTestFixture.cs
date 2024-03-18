
using Wake.Commerce.Application.UseCases.UpdateProduct;
using Wake.Commerce.UnitTests.Application.Common;
using Xunit;

namespace Wake.Commerce.UnitTests.Application.UpdateProduct;

[CollectionDefinition(nameof(UpdateProductTestFixture))]
public class UpdateProductTestFixtureCollection
    : ICollectionFixture<UpdateProductTestFixture>
{ }

public class UpdateProductTestFixture
    : ProductUseCasesBaseFixture
{
    public UpdateProductInput GetValidInput(Guid? id = null)
        => new(
            id ?? Guid.NewGuid(),
            GetValidProductName(),
            GetValidProductStock(),
            GetValidProductPrice()
        );

    public UpdateProductInput GetInvalidInputShortName()
    {
        var invalidInputShortName = GetValidInput();
        if (invalidInputShortName.Name != null) 
        {
            invalidInputShortName.Name = invalidInputShortName.Name[..2];
        }

        return invalidInputShortName;
    }

    public UpdateProductInput GetInvalidInputTooLongName()
    {
        var invalidInputTooLongName = GetValidInput();
        var tooLongNameForProduct = Faker.Commerce.ProductName();
        while (tooLongNameForProduct.Length <= 255)
            tooLongNameForProduct = $"{tooLongNameForProduct} {Faker.Commerce.ProductName()}";
        invalidInputTooLongName.Name = tooLongNameForProduct;
        return invalidInputTooLongName;
    }
}
