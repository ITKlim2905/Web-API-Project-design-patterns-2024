using WebApiOrderFood.BusinessLogic.Dtos;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Mapper;
using WebApiOrderFood.DataAccess.Repositories.Order;

namespace WebApiOrderFood.BusinessLogic.Services;

public class OrderCommandService : IOrderCommandService
{
    private readonly IOrderRepository _orderRepository;

    public OrderCommandService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Create(OrderDto order)
    {
        var entity = OrderMapper.ToEntity(order);
        await _orderRepository.Create(entity);
    }

    public async Task Update(OrderDto order)
    {
        var entity = OrderMapper.ToEntity(order);
        await _orderRepository.Update(entity);
    }

    public async Task Delete(string orderId)
    {
        await _orderRepository.Delete(orderId);
    }
}
