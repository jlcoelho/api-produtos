using Wake.Commerce.IntegrationTests.Application.UseCases;

namespace Wake.Commerce.IntegrationTests.UseCases;

public class UpdateProductTestDateGenerator
{
    public static IEnumerable<object[]> GetProductsToUpdate(int times = 10)
    {
        var fixture = new ProductTestUseCaseFixture();
        for (int indice = 0; indice < times; indice++)
        {
            var exampleProduct = fixture.GetExampleProduct();
            var exampleInput = fixture.GetValidInput(exampleProduct.Id);
            yield return new object[] {
                exampleProduct, exampleInput
            };
        }
    }
}