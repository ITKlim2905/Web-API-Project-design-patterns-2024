﻿using System.Collections.ObjectModel;
using WebApiOrderFoodClean.Domain.Entities;

namespace WebApiOrderFoodClean.Infrastructure.Repositories.Order;

public interface IOrderRepository
{
    public Task<ReadOnlyCollection<OrderEntity>> Get();
    public Task<OrderEntity?> Get(string id);
    public Task<ReadOnlyCollection<OrderEntity>> Get(Func<OrderEntity, bool> predicate);
    public Task Create(OrderEntity entity);
    public Task Update(OrderEntity entity);
    public Task Delete(string OrderId);
}
