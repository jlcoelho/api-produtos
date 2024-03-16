using FluentAssertions;
using Wake.Commerce.Domain.Entity;
using Wake.Commerce.Domain.Exceptions;
using Xunit;

namespace Wake.Commerce.UnitTests.Domain.Entity;

[Collection(nameof(ProductTestFixture))]
public class ProductTest
{
    private readonly ProductTestFixture _productTestFixture;

    public ProductTest(ProductTestFixture productTestFixture)
        => _productTestFixture = productTestFixture;

    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Product - Entity")]
    public void Instantiate()
    {
        var validProduct = _productTestFixture.GetValidProduct();
        var product = new Product(validProduct.Name, validProduct.Stock, validProduct.Price);
        product.Should().NotBeNull();
        product.Name.Should().Be(validProduct.Name);
        product.Stock.Should().Be(validProduct.Stock);
        product.Price.Should().Be(validProduct.Price);
        product.Id.Should().NotBeEmpty();
        product.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Product - Entity")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void InstantiateErrorWhenNameIsEmpty(string? name)
    {
        var validProduct =_productTestFixture.GetValidProduct();

        Action action = () => new Product(name!, validProduct.Stock, validProduct.Price);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenPriceLessThanZero))]
    [Trait("Domain", "Product - Entity")]
    public void InstantiateErrorWhenPriceLessThanZero()
    {
        var validProduct =_productTestFixture.GetValidProduct();

        Action action = () => new Product(validProduct.Name, validProduct.Stock, -1);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Price should not be less than 0");
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan4Characters))]
    [Trait("Domain", "Product - Entity")]
    [InlineData("ab")]
    [InlineData("abc")]
    public void InstantiateErrorWhenNameIsLessThan4Characters(string invalidName)
    {
        var validProduct =_productTestFixture.GetValidProduct();

        Action action = () => new Product(invalidName, validProduct.Stock, validProduct.Price);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be at least 4 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("Domain", "Product - Entity")]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        var validProduct =_productTestFixture.GetValidProduct();

        var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());
        Action action = () => new Product(invalidName, validProduct.Stock, validProduct.Price);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should be less or equal 255 characters long");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenStockLessThanZero))]
    [Trait("Domain", "Product - Entity")]
    public void InstantiateErrorWhenStockLessThanZero()
    {
        var validProduct =_productTestFixture.GetValidProduct();
        
        Action action = () => new Product(validProduct.Name, -1, validProduct.Price);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Stock should not be less than 0");
    }
}