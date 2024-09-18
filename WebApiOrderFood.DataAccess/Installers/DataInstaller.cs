using Microsoft.Extensions.DependencyInjection;
using WebApiOrderFood.DataAccess.Repositories.Order;

namespace WebApiOrderFood.DataAccess.Installers;

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
