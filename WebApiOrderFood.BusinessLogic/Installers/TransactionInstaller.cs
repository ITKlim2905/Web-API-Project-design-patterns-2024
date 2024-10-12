using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WebApiOrderFood.BusinessLogic.Adapters;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Services;
using WebApiOrderFood.BusinessLogic.Factories;
using WebApiOrderFood.DataAccess.Repositories.Order;
using WebApiOrderFood.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace WebApiOrderFood.BusinessLogic.Installers;

public static class TransactionInstaller
{
    public static IServiceCollection AddTransactions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.Decorate<ITransactionService, TransactionServiceDecorator>();
        services.AddScoped<IAdapterTransactionSystem, TransactionAdapter>();
        services.AddDbContext<OrderContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return services;
    }
}