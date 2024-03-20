using FluentAssertions;
using Moq;
using Wake.Commerce.Application.Exceptions;
using Xunit;
using UseCase = Wake.Commerce.Application.UseCases.DeleteProduct;

namespace Wake.Commerce.UnitTests.Application.DeleteProduct;

[Collection(nameof(DeleteProductTestFixture))]
public class DeleteProductTest
{
    private readonly DeleteProductTestFixture _fixture;

    public DeleteProductTest(DeleteProductTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(DeleteProduct))]
    [Trait("Application", "DeleteProduct - Use Cases")]
    public async Task DeleteProduct()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var productExample = _fixture.GetExampleProduct();
        repositoryMock.Setup(x => x.Get(
            productExample.Id,
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(productExample);
        var input = new UseCase.DeleteProductInput(productExample.Id);
        var useCase = new UseCase.DeleteProduct(
            repositoryMock.Object,
            unitOfWorkMock.Object);

        await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(x => x.Get(
            productExample.Id,
            It.IsAny<CancellationToken>()
        ), Times.Once);
        repositoryMock.Verify(x => x.Delete(
            productExample,
            It.IsAny<CancellationToken>()
        ), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }


    [Fact(DisplayName = nameof(ThrowWhenProductNotFound))]
    [Trait("Application", "DeleteProduct - Use Cases")]
    public async Task ThrowWhenProductNotFound()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var exampleGuid = Guid.NewGuid();
        repositoryMock.Setup(x => x.Get(
            exampleGuid,
            It.IsAny<CancellationToken>())
        ).ThrowsAsync(
            new NotFoundException($"Product '{exampleGuid}' not found")
        );
        var input = new UseCase.DeleteProductInput(exampleGuid);
        var useCase = new UseCase.DeleteProduct(
            repositoryMock.Object,
            unitOfWorkMock.Object);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<NotFoundException>();

        repositoryMock.Verify(x => x.Get(
            exampleGuid,
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }
}
