using Wake.Commerce.UnitTests.Application.Common;
using Xunit;

namespace Wake.Commerce.UnitTests.Application.DeleteProduct;

[CollectionDefinition(nameof(DeleteProductTestFixture))]
public class DeleteProductTestFixtureCollection
    : ICollectionFixture<DeleteProductTestFixture>
{ }

public class DeleteProductTestFixture
    : ProductUseCasesBaseFixture
{ }