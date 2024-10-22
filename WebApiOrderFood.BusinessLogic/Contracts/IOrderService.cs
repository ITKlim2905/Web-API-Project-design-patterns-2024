﻿using WebApiOrderFood.BusinessLogic.Dtos;

namespace WebApiOrderFood.BusinessLogic.Contracts;

public interface IOrderService
{
    Task<IReadOnlyList<OrderDto>> Get();
    Task<OrderDto> Get(string orderId);
    Task Add(OrderDto order);
    Task Update(OrderDto order);
    Task Remove(string orderId);
}
