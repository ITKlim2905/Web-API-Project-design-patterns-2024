﻿using System.Collections.ObjectModel;
using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.DataAccess.Repositories.Order;

public class OrderRepository : IOrderRepository
{
    private readonly OrderContext _context;
    public OrderRepository(OrderContext context)
    {
        _context = context;
    }

    public Task<ReadOnlyCollection<OrderEntity>> Get() =>
        Task.FromResult(_context.Orders.ToList().AsReadOnly());

    public Task<OrderEntity?> Get(string OrderID) =>
        Task.FromResult(_context.Orders.FirstOrDefault(e => e.OrderID == OrderID));

    public Task<ReadOnlyCollection<OrderEntity>> Get(Func<OrderEntity, bool> predicate)
    {
        return Task.FromResult(_context.Orders.Where(predicate).ToList().AsReadOnly());
    }

    public Task Create(OrderEntity entity)
    {
        _context.Orders.Add(entity);
        return Task.CompletedTask;
    }

    public Task Update(OrderEntity entity)
    {
        foreach (var e in _context.Orders)
        {
            if (e.OrderID == entity.OrderID)
            {
                e.OrderType = entity.OrderType;
                e.DishType = entity.DishType;
                e.DishName = entity.DishName;
                e.Amount = entity.Amount;
                e.OrderTime = entity.OrderTime;
            }
        }
        return Task.CompletedTask;
    }

    public Task Delete(string OrderID)
    {
        var entity = _context.Orders.FirstOrDefault(e => e.OrderID == OrderID);
        if (entity != null)
        {
            _context.Orders.Remove(entity);
        }
        return Task.CompletedTask;
    }
}