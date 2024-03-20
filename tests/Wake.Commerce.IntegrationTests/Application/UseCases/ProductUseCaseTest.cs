
using ApplicationUseCases = Wake.Commerce.Application.UseCases;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Wake.Commerce.Infra.Data.EF.Repositories;
using Wake.Commerce.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;
using Wake.Commerce.Application.UseCases.DeleteProduct;
using Wake.Commerce.IntegrationTests.UseCases;
using Wake.Commerce.Domain.Entity;
using Wake.Commerce.Application.UseCases.UpdateProduct;
using Wake.Commerce.Application.UseCases.ListProducts;
using Wake.Commerce.Application.UseCases.Common;

namespace Wake.Commerce.IntegrationTests.Application.UseCases;

[Collection(nameof(ProductTestUseCaseFixture))]
public class CreateProductTest
{
    private readonly ProductTestUseCaseFixture _fixture;

    public CreateProductTest(ProductTestUseCaseFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreateProduct))]
    [Trait("Integration/Application", "CreateProduct - Use Cases")]
    public async void CreateProduct()
    {
        var dbContext = _fixture.CreateDbContext();
        var repository = new ProductRepository(dbContext);
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
       
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.CreateProduct.CreateProduct(
            repository, unitOfWork
        );
        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        var dbProduct = await (_fixture.CreateDbContext(true))
            .Products.FindAsync(output.Id);
        dbProduct.Should().NotBeNull();
        dbProduct!.Name.Should().Be(input.Name);
        dbProduct.Stock.Should().Be(input.Stock);
        dbProduct.Price.Should().Be(input.Price);
        dbProduct.CreatedAt.Should().Be(output.CreatedAt);
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Stock.Should().Be(input.Stock);
        output.Price.Should().Be(input.Price);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default);
    }
    
    [Fact(DisplayName = nameof(DeleteProduct))]
    [Trait("Integration/Application", "DeleteProduct - Use Cases")]
    public async Task DeleteProduct()
    {
        var dbContext = _fixture.CreateDbContext();
        var productExample = _fixture.GetExampleProduct();
        var exampleList = _fixture.GetExampleProductsList(10);
        await dbContext.AddRangeAsync(exampleList);
        var tracking = await dbContext.AddAsync(productExample);
        await dbContext.SaveChangesAsync();
        tracking.State = EntityState.Detached;
        var repository = new ProductRepository(dbContext);
 
        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.DeleteProduct.DeleteProduct(
            repository, unitOfWork
        );
        var input = new DeleteProductInput(productExample.Id);

        await useCase.Handle(input, CancellationToken.None);

        var assertDbContext = _fixture.CreateDbContext(true);
        var dbProductDeleted = await assertDbContext.Products
            .FindAsync(productExample.Id);
        dbProductDeleted.Should().BeNull();
        var dbProducts = await assertDbContext
            .Products.ToListAsync();
        dbProducts.Should().HaveCount(exampleList.Count);
    }

    [Fact(DisplayName = nameof(GetProduct))]
    [Trait("Integration/Application", "GetProduct - Use Cases")]
    public async Task GetProduct()
    {
        var exampleProduct = _fixture.GetExampleProduct();
        var dbContext = _fixture.CreateDbContext();
        dbContext.Products.Add(exampleProduct);
        dbContext.SaveChanges();
        var repository = new ProductRepository(dbContext);
        var input = new ApplicationUseCases.GetProduct.GetProductInput(exampleProduct.Id);
        var useCase = new ApplicationUseCases.GetProduct.GetProduct(repository);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(exampleProduct.Name);
        output.Stock.Should().Be(exampleProduct.Stock);
        output.Price.Should().Be(exampleProduct.Price);
        output.Id.Should().Be(exampleProduct.Id);
        output.CreatedAt.Should().Be(exampleProduct.CreatedAt);
    }

    [Theory(DisplayName = nameof(UpdateProduct))]
    [Trait("Integration/Application", "UpdateProduct - Use Cases")]
    [MemberData(
        nameof(UpdateProductTestDateGenerator.GetProductsToUpdate),
        parameters: 5,
        MemberType = typeof(UpdateProductTestDateGenerator)
    )]
    public async Task UpdateProduct(
        Product exampleProduct,
        UpdateProductInput input
    )
    {
        var dbContext = _fixture.CreateDbContext();
        await dbContext.AddRangeAsync(_fixture.GetExampleProductsList());
        var trackingInfo = await dbContext.AddAsync(exampleProduct);
        dbContext.SaveChanges();
        trackingInfo.State = EntityState.Detached;
        var repository = new ProductRepository(dbContext);
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var unitOfWork = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.UpdateProduct.UpdateProduct(repository, unitOfWork);

        var output = await useCase.Handle(input, CancellationToken.None);

        var dbProduct = await (_fixture.CreateDbContext(true))
            .Products.FindAsync(output.Id);
        dbProduct.Should().NotBeNull();
        dbProduct!.Name.Should().Be(input.Name);
        dbProduct.Stock.Should().Be(input.Stock);
        dbProduct.Price.Should().Be((decimal)input.Price!);
        dbProduct.CreatedAt.Should().Be(output.CreatedAt);
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Stock.Should().Be(input.Stock);
        output.Price.Should().Be((decimal)input.Price!);
    }

    [Fact(DisplayName = nameof(SearchRetursListAndTotal))]
    [Trait("Integration/Application", "ListProducts - Use Cases")]
    public async Task SearchRetursListAndTotal()
    {
        WakeCommerceDbContext dbContext = _fixture.CreateDbContext();
        var exampleProductsList = _fixture.GetExampleProductsList(10);
        await dbContext.AddRangeAsync(exampleProductsList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var productRepository = new ProductRepository(dbContext);
        var input = new ListProductsInput(1, 20);
        var useCase = new ApplicationUseCases.ListProducts.ListProducts(
            productRepository
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(input.Page);
        output.PerPage.Should().Be(input.PerPage);
        output.Total.Should().Be(exampleProductsList.Count);
        output.Items.Should().HaveCount(exampleProductsList.Count);
        foreach (ProductOutput outputItem in output.Items)
        {
            var exampleItem = exampleProductsList.Find(
                product => product.Id == outputItem.Id
            );
            exampleItem.Should().NotBeNull();
            outputItem.Name.Should().Be(exampleItem!.Name);
            outputItem.Stock.Should().Be(exampleItem.Stock);
            outputItem.Price.Should().Be(exampleItem.Price);
            outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);
        }
    }

    [Fact(DisplayName = nameof(SearchReturnsEmptyWhenEmpty))]
    [Trait("Integration/Application", "ListProducts - Use Cases")]
    public async Task SearchReturnsEmptyWhenEmpty()
    {
        WakeCommerceDbContext dbContext = _fixture.CreateDbContext();
        var productRepository = new ProductRepository(dbContext);
        var input = new ListProductsInput(1, 20);
        var useCase = new ApplicationUseCases.ListProducts.ListProducts(
            productRepository
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(input.Page);
        output.PerPage.Should().Be(input.PerPage);
        output.Total.Should().Be(0);
        output.Items.Should().HaveCount(0);
    }

    [Theory(DisplayName = nameof(SearchRetursPaginated))]
    [Trait("Integration/Application", "ListProducts - Use Cases")]
    [InlineData(10, 1, 5, 5)]
    [InlineData(10, 2, 5, 5)]
    [InlineData(7, 2, 5, 2)]
    [InlineData(7, 3, 5, 0)]
    public async Task SearchRetursPaginated(
        int quantityProductsToGenerate,
        int page,
        int perPage,
        int expectedQuantityItems
    )
    {
        WakeCommerceDbContext dbContext = _fixture.CreateDbContext();
        var exampleProductsList = _fixture.GetExampleProductsList(
            quantityProductsToGenerate
        );
        await dbContext.AddRangeAsync(exampleProductsList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var productRepository = new ProductRepository(dbContext);
        var input = new ListProductsInput(page, perPage);
        var useCase = new ApplicationUseCases.ListProducts.ListProducts(
            productRepository
        );

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.Page.Should().Be(input.Page);
        output.PerPage.Should().Be(input.PerPage);
        output.Total.Should().Be(exampleProductsList.Count);
        output.Items.Should().HaveCount(expectedQuantityItems);
        foreach (ProductOutput outputItem in output.Items)
        {
            var exampleItem = exampleProductsList.Find(
                product => product.Id == outputItem.Id
            );
            exampleItem.Should().NotBeNull();
            outputItem.Name.Should().Be(exampleItem!.Name);
            outputItem.Stock.Should().Be(exampleItem.Stock);
            outputItem.Price.Should().Be(exampleItem.Price);
            outputItem.CreatedAt.Should().Be(exampleItem.CreatedAt);
        }
    }
}