using WebApiOrderFoodClean.Adapters;
using WebApiOrderFoodClean.Domain.Contracts;
using WebApiOrderFoodClean.Application.Services;
using WebApiOrderFoodClean.Application.Strategy;
using WebApiOrderFoodClean.Application.Mediator;
using WebApiOrderFoodClean.Application.Installers;
using WebApiOrderFoodClean.Infrastructure.Decorators;
using WebApiOrderFoodClean.Infrastructure.Repositories.Order;
using WebApiOrderFoodClean.Application.Commands;
using Microsoft.EntityFrameworkCore;
using WebApiOrderFoodClean.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOrders(builder.Configuration);
builder.Services.AddTransactions(builder.Configuration);
builder.Services.AddFacades();
builder.Services.AddLogging();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(provider => new Lazy<IOrderService>(() => provider.GetRequiredService<IOrderService>()));
builder.Services.AddScoped(provider => new Lazy<ITransactionService>(() => provider.GetRequiredService<ITransactionService>()));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddScoped<OrderQueryService>();
builder.Services.AddScoped<CreateOrderCommand>();
builder.Services.AddTransient<IAdapterTransactionSystem, TransactionAdapter>();
builder.Services.Decorate<IOrderService, OrderServiceDecorator>();
builder.Services.Decorate<ITransactionService, TransactionServiceDecorator>();
builder.Services.AddTransient<LegacyTransactionAdapter>();
builder.Services.AddTransient<NewTransactionAdapter>();
builder.Services.AddTransient<LegacyTransactionSystem>();
builder.Services.AddTransient<NewTransactionSystem>();
builder.Services.AddTransient<IOrderDeliveryTypeStrategy, InTheEstablishmentOrderStrategy>();
builder.Services.AddTransient<IOrderDeliveryTypeStrategy, ForTakeawayOrderStrategy>();

builder.Services.AddTransient<ServiceResolver>(serviceProvider => key =>
{
    switch (key)
    {
        case "InTheEstablishment":
            return serviceProvider.GetRequiredService<InTheEstablishmentOrderStrategy>();
        case "ForTakeaway":
            return serviceProvider.GetRequiredService<ForTakeawayOrderStrategy>();
        default:
            throw new KeyNotFoundException($"Strategy '{key}' is not registered.");
    }
});

builder.Services.AddScoped<IOrderQueryService>(provider =>
{
    var dbContext = provider.GetRequiredService<AppDbContext>();
    return new OrderQueryService(dbContext);
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddTransient<IOrderDeliveryTypeStrategy, DeliveryOrderStrategy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
