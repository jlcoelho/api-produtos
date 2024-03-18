namespace Wake.Commerce.UnitTests.Application.UpdateProduct;
public class UpdateProductTestDataGenerator
{
    public static IEnumerable<object[]> GetProductsToUpdate(int times = 10)
    {
        var fixture = new UpdateProductTestFixture();
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
