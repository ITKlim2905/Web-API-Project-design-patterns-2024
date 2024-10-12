using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Services;
using WebApiOrderFood.BusinessLogic.Factories;
using WebApiOrderFood.DataAccess.Repositories.Order;
using WebApiOrderFood.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace WebApiOrderFood.BusinessLogic.Installers;

public static class OrderInstaller
{
    public static IServiceCollection AddOrders(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderService, OrderService>();
        services.Decorate<IOrderService, OrderServiceDecorator>();
        services.AddScoped<InTheEstablishmentOrderFactory>();
        services.AddScoped<ForTakeawayOrderFactory>();
        services.AddScoped<DeliveryOrderFactory>();
        services.AddDbContext<OrderContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}
