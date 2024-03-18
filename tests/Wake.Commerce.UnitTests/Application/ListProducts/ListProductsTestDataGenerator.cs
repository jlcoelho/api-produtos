using Wake.Commerce.Application.UseCases.ListProducts;

namespace Wake.Commerce.UnitTests.Application.ListProducts;
public class ListProductsTestDataGenerator
{
    public static IEnumerable<object[]> GetInputsWithoutAllParameter(int times = 14)
    {
        var fixture = new ListProductsTestFixture();
        var inputExample = fixture.GetExampleInput();
        for (int i = 0; i < times; i++)
        {
            switch (i % 7)
            {
                case 0:
                    yield return new object[] {
                        new ListProductsInput()
                    };
                    break;
                case 1:
                    yield return new object[] {
                        new ListProductsInput(inputExample.Page)
                    };
                    break;
                case 3:
                    yield return new object[] {
                        new ListProductsInput(
                            inputExample.Page,
                            inputExample.PerPage
                        )
                    };
                    break;
                case 4:
                    yield return new object[] {
                        new ListProductsInput(
                            inputExample.Page,
                            inputExample.PerPage,
                            inputExample.Search
                        )
                    };
                    break;
                case 5:
                    yield return new object[] {
                        new ListProductsInput(
                            inputExample.Page,
                            inputExample.PerPage,
                            inputExample.Search,
                            inputExample.Sort
                        )
                    };
                    break;
                case 6:
                    yield return new object[] { inputExample };
                    break;
                default:
                    yield return new object[] {
                        new ListProductsInput()
                    };
                    break;
            }
        }
    }
}
