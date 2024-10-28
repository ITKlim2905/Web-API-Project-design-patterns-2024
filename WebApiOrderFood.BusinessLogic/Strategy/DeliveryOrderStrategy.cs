using WebApiOrderFood.BusinessLogic.Dtos;
using WebApiOrderFood.BusinessLogic.Services;

namespace WebApiOrderFood.BusinessLogic.Strategy;

public class DeliveryOrderStrategy
{
    public void OrderDeliveryType(OrderDto order, decimal amount)
    {
        order.UpdateOrder(amount);
    }
}