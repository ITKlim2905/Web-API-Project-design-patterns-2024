using Microsoft.Extensions.DependencyInjection;
using WebApiOrderFoodClean.Infrastructure.Repositories;
using WebApiOrderFoodClean.Infrastructure.Repositories.Order;

namespace WebApiOrderFoodClean.Infrastructure.Installers;

public static class DataInstaller
{
    public static IServiceCollection AddDataContext(this IServiceCollection services)
    {
        services
            .AddSingleton<OrderContext>()
            .AddTransient<ITransactionRepository, TransactionRepository>()
            .AddTransient<IOrderRepository, OrderRepository>();

        return services;
    }
}
