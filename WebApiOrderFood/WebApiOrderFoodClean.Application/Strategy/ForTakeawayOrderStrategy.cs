using WebApiOrderFoodClean.Domain.Dtos;

namespace WebApiOrderFoodClean.Application.Strategy;

public class ForTakeawayOrderStrategy : IOrderDeliveryTypeStrategy
{
    private readonly ServiceResolver _serviceResolver;

    public ForTakeawayOrderStrategy(ServiceResolver serviceResolver)
    {
        _serviceResolver = serviceResolver;
    }
    public void OrderDeliveryType(OrderDto order, decimal amount)
    {
        order.UpdateOrder(amount);
    }
}