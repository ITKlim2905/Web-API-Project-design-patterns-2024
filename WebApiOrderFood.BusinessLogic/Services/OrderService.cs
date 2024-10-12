using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Dtos;
using WebApiOrderFood.DataAccess.Entities;
using WebApiOrderFood.DataAccess.Repositories.Order;

namespace WebApiOrderFood.BusinessLogic.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
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
}
