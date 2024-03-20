using Bogus;
using Microsoft.EntityFrameworkCore;
using Wake.Commerce.Infra.Data.EF;

namespace Wake.Commerce.IntegrationTests.Base;

public class BaseFixture
{
    public BaseFixture() 
        => Faker = new Faker("pt_BR");

    protected Faker Faker { get; set; }

    public WakeCommerceDbContext CreateDbContext(bool preserveData = false)
    {
        var context = new WakeCommerceDbContext(
            new DbContextOptionsBuilder<WakeCommerceDbContext>()
            .UseInMemoryDatabase("integration-tests-db")
            .Options
        );
        if (preserveData == false)
            context.Database.EnsureDeleted();
        return context;
    }
}