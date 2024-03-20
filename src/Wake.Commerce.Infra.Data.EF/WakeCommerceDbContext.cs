
using Microsoft.EntityFrameworkCore;
using Wake.Commerce.Domain.Entity;
using Wake.Commerce.Infra.Data.EF.Configurations;

namespace Wake.Commerce.Infra.Data.EF;

public class WakeCommerceDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public WakeCommerceDbContext(DbContextOptions<WakeCommerceDbContext> options) 
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new ProductConfiguration());
    }
}