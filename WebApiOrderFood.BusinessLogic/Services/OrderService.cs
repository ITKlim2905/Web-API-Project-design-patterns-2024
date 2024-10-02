﻿using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Dtos;
using WebApiOrderFood.BusinessLogic.Factories;
using WebApiOrderFood.DataAccess.Entities;
using WebApiOrderFood.DataAccess.Repositories.Order;

namespace WebApiOrderFood.BusinessLogic.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly InTheEstablishmentOrderFactory _inTheEstablishmentOrderFactory;
    private readonly ForTakeawayOrderFactory _forTakeawayOrderFactory;
    private readonly DeliveryOrderFactory _deliveryOrderFactory;

    public OrderService(
            IOrderRepository orderRepository,
            InTheEstablishmentOrderFactory inTheEstablishmentOrderFactory,
            ForTakeawayOrderFactory forTakeawayOrderFactory,
            DeliveryOrderFactory deliveryOrderFactory)
    {
        _orderRepository = orderRepository;
        _inTheEstablishmentOrderFactory = inTheEstablishmentOrderFactory;
        _forTakeawayOrderFactory = forTakeawayOrderFactory;
        _deliveryOrderFactory = deliveryOrderFactory;
    }

    public async Task<IReadOnlyList<OrderDto>> Get()
    {
        var orders = await _orderRepository.Get();
        if (orders == null)
            return new List<OrderDto>();

        return orders.Select(e => new OrderDto(
            orderId: e.OrderId,
            orderType: e.OrderType,
            dishType: e.DishType,
            dishName: e.DishName,
            amount: e.Amount,
            orderTime: e.OrderTime)).ToList().AsReadOnly();
    }

    public async Task<OrderDto> Get(string orderId)
    {
        var order = await _orderRepository.Get(orderId);
        if (order == null)
            return OrderDto.Default;
        return new OrderDto(
            orderId: order.OrderId,
            orderType: order.OrderType,
            dishType: order.DishType,
            dishName: order.DishName,
            amount: order.Amount,
            orderTime: order.OrderTime);
    }

    public async Task Add(OrderDto order)
    {
        if (order == OrderDto.Default)
            return;

        IOrder newOrder;

        switch (order.OrderType)
        {
            case OrderType.InTheEstablishment:
                newOrder = _inTheEstablishmentOrderFactory.CreateOrder(order.OrderId, order.DishType, order.DishName, order.Amount, DateTime.UtcNow);
                break;
            case OrderType.ForTakeaway:
                newOrder = _forTakeawayOrderFactory.CreateOrder(order.OrderId, order.DishType, order.DishName, order.Amount, DateTime.UtcNow);
                break;
            case OrderType.Delivery:
                newOrder = _deliveryOrderFactory.CreateOrder(order.OrderId, order.DishType, order.DishName, order.Amount, DateTime.UtcNow);
                break;
            default:
                throw new InvalidOperationException("Unknown order type.");
        }

        await _orderRepository.Create(new OrderEntity
        {
            OrderId = order.OrderId,
            OrderType = order.OrderType,
            DishType = order.DishType,
            DishName = order.DishName,
            Amount = order.Amount,
            OrderTime = DateTime.UtcNow
        });
    }

    public async Task Update(OrderDto order)
    {
        if (order == OrderDto.Default)
            return;

        await _orderRepository.Update(new OrderEntity
        {
            OrderId = order.OrderId,
            OrderType = order.OrderType,
            DishType = order.DishType,
            DishName = order.DishName,
            Amount = order.Amount,
            OrderTime = DateTime.UtcNow
        });
    }

    public async Task Remove(string orderId)
    {
        if (string.IsNullOrEmpty(orderId))
            throw new ArgumentNullException();

        await _orderRepository.Delete(orderId);
    }

    public async Task<OrderDto> CloneOrder(string OrderId)
    {
        var existingOrder = await _orderRepository.Get(OrderId);
        if (existingOrder == null)
            throw new InvalidOperationException($"Order with Id {OrderId} not found.");

        var existingOrderDto = new OrderDto(
            orderId: existingOrder.OrderId,
            orderType: existingOrder.OrderType,
            dishType: existingOrder.DishType,
            dishName: existingOrder.DishName,
            amount: existingOrder.Amount,
            orderTime: existingOrder.OrderTime
        );

        IOrder cloneOrder;
        switch (existingOrderDto.OrderType)
        {
            case OrderType.InTheEstablishment:
                cloneOrder = _inTheEstablishmentOrderFactory.CreateOrder(
                    Guid.NewGuid().ToString(),
                    existingOrderDto.DishType,
                    existingOrderDto.DishName,
                    existingOrderDto.Amount,
                    DateTime.UtcNow);
                break;
            case OrderType.ForTakeaway:
                cloneOrder = _forTakeawayOrderFactory.CreateOrder(
                    Guid.NewGuid().ToString(),
                    existingOrderDto.DishType,
                    existingOrderDto.DishName,
                    existingOrderDto.Amount,
                    DateTime.UtcNow);
                break;
            case OrderType.Delivery:
                cloneOrder = _deliveryOrderFactory.CreateOrder(
                    Guid.NewGuid().ToString(),
                    existingOrderDto.DishType,
                    existingOrderDto.DishName,
                    existingOrderDto.Amount,
                    DateTime.UtcNow);
                break;
            default:
                throw new InvalidOperationException("Unknown order type.");
        }

        await _orderRepository.Create(new OrderEntity
        {
            OrderId = cloneOrder.OrderId,
            OrderType = cloneOrder.OrderType,
            DishType = cloneOrder.DishType,
            DishName = cloneOrder.DishName,
            Amount = cloneOrder.Amount,
            OrderTime = cloneOrder.OrderTime
        });

        return new OrderDto(
                cloneOrder.OrderId,
                cloneOrder.OrderType,
                cloneOrder.DishType,
                cloneOrder.DishName,
                cloneOrder.Amount,
                cloneOrder.OrderTime);
    }
}
