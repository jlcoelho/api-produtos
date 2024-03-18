using Wake.Commerce.UnitTests.Application.Common;
using Xunit;

namespace Wake.Commerce.UnitTests.Application.GetProduct;

[CollectionDefinition(nameof(GetProductTestFixture))]
public class GetProductTestFixtureCollection :
    ICollectionFixture<GetProductTestFixture>
{ }

public class GetProductTestFixture
    : ProductUseCasesBaseFixture
{ }
