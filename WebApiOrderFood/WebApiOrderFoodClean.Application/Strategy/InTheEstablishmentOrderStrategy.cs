using WebApiOrderFoodClean.Domain.Dtos;

namespace WebApiOrderFoodClean.Application.Strategy;

public class InTheEstablishmentOrderStrategy : IOrderDeliveryTypeStrategy
{
    private readonly ServiceResolver _serviceResolver;

    public InTheEstablishmentOrderStrategy(ServiceResolver serviceResolver)
    {
        _serviceResolver = serviceResolver;
    }
    public void OrderDeliveryType(OrderDto order, decimal amount)
    {
        order.UpdateOrder(amount);
    }
}
