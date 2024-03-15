using Xunit;

namespace Wake.Commerce.Domain.Entity
{

  public class ProductTest
  {

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Product - Entity")]
    public void Instantiate()
    {
      var validData = new
      {
        Name = "Product Name",
        Stock = 10,
        Price = 10.5m
      };

      var datetimeBefore = DateTime.Now;

      var product = new Product(validData.Name, validData.Stock, validData.Price);
      var datetimeAfter = DateTime.Now;

      Assert.NotNull(product);
      Assert.Equal(validData.Name, product.Name);
      Assert.Equal(validData.Stock, product.Stock);
      Assert.Equal(validData.Price, product.Price);
      Assert.NotEqual(default(Guid), product.Id);
      Assert.NotEqual(default(DateTime), product.CreatedAt);
      Assert.True(product.CreatedAt > datetimeBefore);
      Assert.True(product.CreatedAt < datetimeAfter);
    }
  }
}

