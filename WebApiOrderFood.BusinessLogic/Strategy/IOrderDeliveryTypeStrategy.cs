using WebApiOrderFood.BusinessLogic.Dtos;

namespace WebApiOrderFood.BusinessLogic.Strategy;

public interface IOrderDeliveryTypeStrategy
{
    void OrderDeliveryType(OrderDto order, decimal amount);
}
