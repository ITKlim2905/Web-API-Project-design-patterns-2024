using Microsoft.Extensions.DependencyInjection;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Services;

namespace WebApiOrderFood.BusinessLogic.Installers;

public static class OrderInstaller
{
    public static IServiceCollection AddOrders(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        return services;
    }
}