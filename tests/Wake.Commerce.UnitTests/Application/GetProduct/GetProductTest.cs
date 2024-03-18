using FluentAssertions;
using Moq;
using Wake.Commerce.Application.Exceptions;
using Xunit;
using UseCase = Wake.Commerce.Application.UseCases.GetProduct;

namespace Wake.Commerce.UnitTests.Application.GetProduct;

[Collection(nameof(GetProductTestFixture))]
public class GetProductTest
{
    private readonly GetProductTestFixture _fixture;

    public GetProductTest(GetProductTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(GetProduct))]
    [Trait("Application", "GetProduct - Use Cases")]
    public async Task GetProduct()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var exampleProduct = _fixture.GetExampleProduct();
        repositoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(exampleProduct);
        var input = new UseCase.GetProductInput(exampleProduct.Id);
        var useCase = new UseCase.GetProduct(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);

        output.Should().NotBeNull();
        output.Name.Should().Be(exampleProduct.Name);
        output.Stock.Should().Be(exampleProduct.Stock);
        output.Price.Should().Be(exampleProduct.Price);
        output.Id.Should().Be(exampleProduct.Id);
        output.CreatedAt.Should().Be(exampleProduct.CreatedAt);
    }

    [Fact(DisplayName = nameof(NotFoundExceptionWhenProductDoesntExist))]
    [Trait("Application", "GetProduct - Use Cases")]
    public async Task NotFoundExceptionWhenProductDoesntExist()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var exampleGuid = Guid.NewGuid();
        repositoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )).ThrowsAsync(
            new NotFoundException($"Product '{exampleGuid}' not found")
        );
        var input = new UseCase.GetProductInput(exampleGuid);
        var useCase = new UseCase.GetProduct(repositoryMock.Object);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();
        repositoryMock.Verify(x => x.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }
}