using Microsoft.Extensions.DependencyInjection;
using WebApiOrderFood.BusinessLogic.Facades;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Services;

namespace WebApiOrderFood.BusinessLogic.Installers;

public static class FacadeInstaller
{
    public static IServiceCollection AddFacades(this IServiceCollection services)
    {
        services.AddScoped<OrderTransactionFacade>();
        return services;
    }
}