using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WebApiOrderFoodClean.Application.Services;
using WebApiOrderFoodClean.Domain.Contracts;
using WebApiOrderFoodClean.Infrastructure.Decorators;
using WebApiOrderFoodClean.Infrastructure.Repositories.Order;
using WebApiOrderFoodClean.Infrastructure.Repositories;

namespace WebApiOrderFoodClean.Application.Installers;

public static class TransactionInstaller
{
    public static IServiceCollection AddTransactions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.Decorate<ITransactionService, TransactionServiceDecorator>();
        services.AddDbContext<OrderContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}