using WebApiOrderFood.BusinessLogic.Dtos;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Mapper;
using WebApiOrderFood.DataAccess.Repositories.Order;

namespace WebApiOrderFood.BusinessLogic.Services;

public class OrderQueryService : IOrderQueryService
{
    private readonly IOrderRepository _orderRepository;

    public OrderQueryService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IReadOnlyList<OrderDto>> Get()
    {
        var orders = await _orderRepository.Get();
        return orders.Select(order => order.ToDto()).ToList();
    }

    public async Task<OrderDto> Get(string orderId)
    {
        var orderEntity = await _orderRepository.Get(orderId);
        return orderEntity?.ToDto();
    }
}