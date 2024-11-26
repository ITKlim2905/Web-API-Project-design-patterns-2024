using WebApiOrderFoodClean.Domain.Dtos;
using WebApiOrderFoodClean.Domain.Contracts;
using WebApiOrderFoodClean.Infrastructure.Mapper;
using WebApiOrderFoodClean.Infrastructure.Repositories.Order;
using Microsoft.EntityFrameworkCore;
using WebApiOrderFoodClean.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace WebApiOrderFoodClean.Application.Services;

public class OrderQueryService : IOrderQueryService
{
    private readonly AppDbContext _context;


    [ActivatorUtilitiesConstructor]
    public OrderQueryService(AppDbContext context)
    {
        _context = context;
    }

    /* public async Task<IReadOnlyList<OrderDto>> Get()
    {
        var orders = await _orderRepository.Get();
        return orders.Select(order => order.ToDto()).ToList();
    }

    public async Task<OrderDto> Get(string orderId)
    {
        var orderEntity = await _orderRepository.Get(orderId);
        return orderEntity?.ToDto();
    } */

    public async Task<IEnumerable<OrderDto>> Get()
    {
        return await _context.Order
            .Select(o => new OrderDto(
                o.OrderId,
                o.OrderType,
                o.DishType,
                o.DishName,
                o.Amount,
                o.OrderTime))
            .ToListAsync();
    }

    public async Task<OrderDto> Get(string orderId)
    {
        var order = await _context.Order.FindAsync(orderId);
        if (order == null) throw new KeyNotFoundException("Order not found");
        return new OrderDto(
            order.OrderId,
            order.OrderType,
            order.DishType,
            order.DishName,
            order.Amount,
            order.OrderTime);
    }
}