using FluentAssertions;
using Moq;
using Wake.Commerce.Application.UseCases.CreateProduct;
using Wake.Commerce.Domain.Entity;
using Wake.Commerce.Domain.Exceptions;
using Xunit;
using Usecases = Wake.Commerce.Application.UseCases.CreateProduct;

namespace Wake.Commerce.UnitTests.Application.CreateProduct;

[Collection(nameof(CreateProductTestFixture))]
public class CreateProductTest
{
    private readonly CreateProductTestFixture _fixture;

    public CreateProductTest(CreateProductTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(CreateProduct))]
    [Trait("Application", "CreateProduct - Use Cases")]
    public async void CreateProduct()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var useCase = new Usecases.CreateProduct(
            repositoryMock.Object, unitOfWorkMock.Object
        );

        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<Product>(), 
                It.IsAny<CancellationToken>()
            ), 
            Times.Once
        );

        unitOfWorkMock.Verify(
            uow => uow.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Stock.Should().Be(input.Stock);
        output.Price.Should().Be(input.Price);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstantianteProduct))]
    [Trait("Application", "CreateProduct - Use Cases")]
    [MemberData(
        nameof(CreateProductTestDataGenerator.GetInvalidInputs), 
        parameters: 50, 
        MemberType = typeof(CreateProductTestDataGenerator)
    )]
    public async void ThrowWhenCantInstantianteProduct(
        CreateProductInput input,
        string exceptionMessage
    )
    {
        var useCase = new Usecases.CreateProduct(
            _fixture.GetRepositoryMock().Object,
            _fixture.GetUnitOfWorkMock().Object
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMessage);
    }
}