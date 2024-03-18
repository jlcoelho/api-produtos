using FluentAssertions;
using Moq;
using Wake.Commerce.Application.UseCases.Common;
using Wake.Commerce.Domain.Entity;
using Wake.Commerce.Domain.SeedWork.SearchableRepository;
using Xunit;
using UseCase = Wake.Commerce.Application.UseCases.ListProducts;

namespace Wake.Commerce.UnitTests.Application.ListProducts;

[Collection(nameof(ListProductsTestFixture))]
public class ListProductsTest
{
    private readonly ListProductsTestFixture _fixture;

    public ListProductsTest(ListProductsTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(List))]
    [Trait("Application", "ListProducts - Use Cases")]
    public async Task List()
    {
        var productsExampleList = _fixture.GetExampleProductsList();
        var repositoryMock = _fixture.GetRepositoryMock();
        var input = _fixture.GetExampleInput();
        var outputRepositorySearch = new SearchOutput<Product>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: (IReadOnlyList<Product>)productsExampleList,
            total: new Random().Next(50, 200)
        );
        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);
        var useCase = new UseCase.ListProducts(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        ((List<ProductOutput>)output.Items).ForEach(outputItem =>
        {
            var repositoryProduct = outputRepositorySearch.Items
                .FirstOrDefault(x => x.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repositoryProduct!.Name);
            outputItem.Stock.Should().Be(repositoryProduct!.Stock);
            outputItem.Price.Should().Be(repositoryProduct!.Price);
            outputItem.CreatedAt.Should().Be(repositoryProduct!.CreatedAt);
        });
        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }


    [Fact(DisplayName = nameof(ListOkWhenEmpty))]
    [Trait("Application", "ListProducts - Use Cases")]
    public async Task ListOkWhenEmpty()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var input = _fixture.GetExampleInput();
        var outputRepositorySearch = new SearchOutput<Product>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: new List<Product>().AsReadOnly(),
            total: 0
        );
        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);
        var useCase = new UseCase.ListProducts(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(0);
        output.Items.Should().HaveCount(0);

        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }

    [Theory(DisplayName = nameof(ListInputWithoutAllParameters))]
    [Trait("Application", "ListProducts - Use Cases")]
    [MemberData(
        nameof(ListProductsTestDataGenerator.GetInputsWithoutAllParameter),
        parameters: 14,
        MemberType = typeof(ListProductsTestDataGenerator)
    )]
    public async Task ListInputWithoutAllParameters(
        UseCase.ListProductsInput input
    )
    {
        var productsExampleList = _fixture.GetExampleProductsList();
        var repositoryMock = _fixture.GetRepositoryMock();
        var outputRepositorySearch = new SearchOutput<Product>(
            currentPage: input.Page,
            perPage: input.PerPage,
            items: (IReadOnlyList<Product>)productsExampleList,
            total: new Random().Next(50, 200)
        );
        repositoryMock.Setup(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(outputRepositorySearch);
        var useCase = new UseCase.ListProducts(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Page.Should().Be(outputRepositorySearch.CurrentPage);
        output.PerPage.Should().Be(outputRepositorySearch.PerPage);
        output.Total.Should().Be(outputRepositorySearch.Total);
        output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
        ((List<ProductOutput>)output.Items).ForEach(outputItem =>
        {
            var repositoryProduct = outputRepositorySearch.Items
                .FirstOrDefault(x => x.Id == outputItem.Id);
            outputItem.Should().NotBeNull();
            outputItem.Name.Should().Be(repositoryProduct!.Name);
            outputItem.Stock.Should().Be(repositoryProduct!.Stock);
            outputItem.Price.Should().Be(repositoryProduct!.Price);
            outputItem.CreatedAt.Should().Be(repositoryProduct!.CreatedAt);
        });
        repositoryMock.Verify(x => x.Search(
            It.Is<SearchInput>(
                searchInput => searchInput.Page == input.Page
                && searchInput.PerPage == input.PerPage
                && searchInput.Search == input.Search
                && searchInput.OrderBy == input.Sort
                && searchInput.Order == input.Dir
            ),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }
}

