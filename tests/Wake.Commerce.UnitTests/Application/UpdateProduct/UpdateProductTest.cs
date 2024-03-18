
using FluentAssertions;
using Moq;
using Wake.Commerce.Application.UseCases.Common;
using Wake.Commerce.Application.UseCases.UpdateProduct;
using Wake.Commerce.Domain.Entity;
using Xunit;
using UseCase = Wake.Commerce.Application.UseCases.UpdateProduct;

namespace Wake.Commerce.UnitTests.Application.UpdateProduct;


[Collection(nameof(UpdateProductTestFixture))]
public class UpdateProductTest
{
    private readonly UpdateProductTestFixture _fixture;

    public UpdateProductTest(UpdateProductTestFixture fixture)
        => _fixture = fixture;

    [Theory(DisplayName = nameof(UpdateProduct))]
    [Trait("Application", "UpdateProduct - Use Cases")]
    [MemberData(
        nameof(UpdateProductTestDataGenerator.GetProductsToUpdate),
        parameters: 10,
        MemberType = typeof(UpdateProductTestDataGenerator)
    )]
    public async Task UpdateProduct(
        Product exampleProduct,
        UpdateProductInput input
    )
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        repositoryMock.Setup(x => x.Get(
            exampleProduct.Id,
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(exampleProduct);

        var useCase = new UseCase.UpdateProduct(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        ProductOutput output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Stock.Should().Be(input.Stock);
        output.Price.Should().Be(input.Price);

        repositoryMock.Verify(x => x.Get(
            exampleProduct.Id,
            It.IsAny<CancellationToken>())
        , Times.Once);

        repositoryMock.Verify(x => x.Update(
            exampleProduct,
            It.IsAny<CancellationToken>())
        , Times.Once);

        unitOfWorkMock.Verify(x => x.Commit(
            It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Theory(DisplayName = nameof(UpdateProductOnlyName))]
    [Trait("Application", "UpdateProduct - Use Cases")]
    [MemberData(
        nameof(UpdateProductTestDataGenerator.GetProductsToUpdate), 
        parameters: 10, 
        MemberType = typeof(UpdateProductTestDataGenerator)
    )]
    public async Task UpdateProductOnlyName(
        Product exampleProduct, 
        UpdateProductInput exampleInput
    )
    {
        var input = new UpdateProductInput(exampleInput.Id, name: exampleInput.Name);
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        repositoryMock.Setup(x => 
            x.Get(exampleProduct.Id, 
            It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(exampleProduct);

        var useCase = new UseCase.UpdateProduct(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        ProductOutput output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Stock.Should().Be(exampleProduct.Stock);
        output.Price.Should().Be(exampleProduct.Price);

        repositoryMock.Verify(x => x.Get(
            exampleProduct.Id,
            It.IsAny<CancellationToken>())
        , Times.Once);

        repositoryMock.Verify(x => x.Update(
            exampleProduct,
            It.IsAny<CancellationToken>())
        , Times.Once);

        unitOfWorkMock.Verify(x => x.Commit(
            It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Theory(DisplayName = nameof(UpdateProductOnlyStock))]
    [Trait("Application", "UpdateProduct - Use Cases")]
    [MemberData(
        nameof(UpdateProductTestDataGenerator.GetProductsToUpdate), 
        parameters: 10, 
        MemberType = typeof(UpdateProductTestDataGenerator)
    )]
    public async Task UpdateProductOnlyStock(
        Product exampleProduct,
        UpdateProductInput exampleInput
    )
    {
        var input = new UpdateProductInput(exampleInput.Id, stock: exampleInput.Stock);
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        repositoryMock.Setup(x => 
            x.Get(exampleProduct.Id, 
            It.IsAny<CancellationToken>()
            )
        ).ReturnsAsync(exampleProduct);

        var useCase = new UseCase.UpdateProduct(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        ProductOutput output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Stock.Should().Be(input.Stock);
        output.Name.Should().Be(exampleProduct.Name);
        output.Price.Should().Be(exampleProduct.Price);

        repositoryMock.Verify(x => x.Get(
            exampleProduct.Id,
            It.IsAny<CancellationToken>())
        , Times.Once);

        repositoryMock.Verify(x => x.Update(
            exampleProduct,
            It.IsAny<CancellationToken>())
        , Times.Once);
        
        unitOfWorkMock.Verify(x => x.Commit(
            It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}