using WebApiOrderFoodClean.Domain.Dtos;

namespace WebApiOrderFoodClean.Application.Strategy;

public class DeliveryOrderStrategy
{
    public void OrderDeliveryType(OrderDto order, decimal amount)
    {
        order.UpdateOrder(amount);
    }
}