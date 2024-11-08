using WebApiOrderFood.BusinessLogic.Installers;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Services;
using WebApiOrderFood.BusinessLogic.Adapters;
using WebApiOrderFood.BusinessLogic.Strategy;
using WebApiOrderFood.BusinessLogic.Mediator;
using WebApiOrderFood.DataAccess.Repositories.Order;

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
builder.Services.AddScoped<IOrderCommandService, OrderCommandService>();
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();
builder.Services.AddTransient<IAdapterTransactionSystem, TransactionAdapter>();
builder.Services.Decorate<IOrderService, OrderServiceDecorator>();
builder.Services.Decorate<ITransactionService, TransactionServiceDecorator>();
builder.Services.AddTransient<LegacyTransactionAdapter>();
builder.Services.AddTransient<NewTransactionAdapter>();
builder.Services.AddTransient<LegacyTransactionSystem>();
builder.Services.AddTransient<NewTransactionSystem>();
builder.Services.AddTransient<IOrderDeliveryTypeStrategy, InTheEstablishmentOrderStrategy>();
builder.Services.AddTransient<IOrderDeliveryTypeStrategy, ForTakeawayOrderStrategy>();
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
