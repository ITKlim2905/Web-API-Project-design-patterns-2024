using WebApiOrderFoodClean.Domain.Dtos;

namespace WebApiOrderFoodClean.Application.Strategy;

public interface IOrderDeliveryTypeStrategy
{
    void OrderDeliveryType(OrderDto order, decimal amount);
}
