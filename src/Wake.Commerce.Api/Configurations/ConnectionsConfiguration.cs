using Microsoft.EntityFrameworkCore;
using Wake.Commerce.Infra.Data.EF;

namespace Wake.Commerce.Api.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection AddConnection(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbConnection(configuration);
        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        var connectionString = configuration.GetConnectionString("WakeCommerceDb");
        services.AddDbContext<WakeCommerceDbContext>(
            options => options.UseSqlServer(
                connectionString
            )
        );
        return services;
    }
}