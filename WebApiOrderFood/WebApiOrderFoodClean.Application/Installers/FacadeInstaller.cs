using Microsoft.Extensions.DependencyInjection;
using WebApiOrderFoodClean.Application.Facades;

namespace WebApiOrderFoodClean.Application.Installers;

public static class FacadeInstaller
{
    public static IServiceCollection AddFacades(this IServiceCollection services)
    {
        services.AddScoped<OrderTransactionFacade>();
        return services;
    }
}