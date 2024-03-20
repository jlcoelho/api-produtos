using MediatR;
using Wake.Commerce.Application.Interfaces;
using Wake.Commerce.Application.UseCases.CreateProduct;
using Wake.Commerce.Domain.Repository;
using Wake.Commerce.Infra.Data.EF;
using Wake.Commerce.Infra.Data.EF.Repositories;

namespace Wake.Commerce.Api.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(
        this IServiceCollection services
    )
    {
        services.AddMediatR(typeof(CreateProduct));
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}