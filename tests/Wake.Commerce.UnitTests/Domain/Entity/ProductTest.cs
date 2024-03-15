using Wake.Commerce.Domain.Exceptions;
using Xunit;

namespace Wake.Commerce.Domain.Entity;

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

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Product - Entity")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        Action action = () => new Product(name!, 10, 10.5m);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be empty or null", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenPriceLessThanZero))]
    [Trait("Domain", "Product - Entity")]
    public void InstantiateErrorWhenPriceLessThanZero()
    {
        Action action = () => new Product("Product Name", 10, -1);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Price should not be less than 0", exception.Message);
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan4Characters))]
    [Trait("Domain", "Product - Entity")]
    [InlineData("ab")]
    [InlineData("abc")]
    public void InstantiateErrorWhenNameIsLessThan4Characters(string invalidName)
    {
        Action action = () => new Product(invalidName, 1, 10.5m);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be at least 4 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Product - Entity")]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
        Action action = () => new Product(invalidName, 1, 10.5m);
        
        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenStockLessThanZero))]
    [Trait("Domain", "Product - Entity")]
    public void InstantiateErrorWhenStockLessThanZero()
    {
        Action action = () => new Product("Product Name", -1, 10.5m);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Stock should not be less than 0", exception.Message);
    }
}