using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WebApiOrderFoodClean.Application.Services;
using WebApiOrderFoodClean.Domain.Contracts;
using WebApiOrderFoodClean.Domain.Factories;
using WebApiOrderFoodClean.Infrastructure.Decorators;
using WebApiOrderFoodClean.Infrastructure.Repositories.Order;
using WebApiOrderFoodClean.Infrastructure.Repositories;

namespace WebApiOrderFoodClean.Application.Installers;

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