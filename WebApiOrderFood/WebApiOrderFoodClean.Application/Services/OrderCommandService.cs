using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using WebApiOrderFoodClean.Domain.Dtos;
using WebApiOrderFoodClean.Domain.Entities;
using WebApiOrderFoodClean.Domain.Factories;
using WebApiOrderFoodClean.Infrastructure.Repositories;

namespace WebApiOrderFoodClean.Application.Commands;

public class CreateOrderCommand
{
    public string OrderId { get; set; }
    public string DishName { get; set; }
    public decimal Amount { get; set; }

    private readonly AppDbContext _context;

    public CreateOrderCommand(AppDbContext context)
    {
        _context = context;
    }

    public async Task Create(OrderDto orderDto)
    {
        var order = new OrderEntity
        {
            OrderId = orderDto.OrderId,
            OrderType = orderDto.OrderType,
            DishType = orderDto.DishType,
            DishName = orderDto.DishName,
            Amount = orderDto.Amount,
            OrderTime = orderDto.OrderTime
        };
        _context.Order.Add(order);
        await _context.SaveChangesAsync();
    }

    public async Task Update(OrderDto orderDto)
    {
        var order = await _context.Order.FindAsync(orderDto.OrderId);
        if (order == null) throw new KeyNotFoundException("Order not found");

        order.OrderType = orderDto.OrderType;
        order.DishType = orderDto.DishType;
        order.DishName = orderDto.DishName;
        order.Amount = orderDto.Amount;

        await _context.SaveChangesAsync();
    }

    public async Task Delete(string orderId)
    {
        var order = await _context.Order.FindAsync(orderId);
        if (order == null) throw new KeyNotFoundException("Order not found");

        _context.Order.Remove(order);
        await _context.SaveChangesAsync();
    }
}