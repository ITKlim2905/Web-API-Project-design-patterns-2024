using WebApiOrderFoodClean.Domain.Entities;

namespace WebApiOrderFoodClean.Domain.Factories;

public interface IOrder
{
    string OrderId { get; }
    OrderType OrderType { get; }
    DishType DishType { get; }
    string DishName { get; }
    decimal Amount { get; }
    DateTime OrderTime { get; }

    void ProcessOrder();
}

public abstract class OrderFactory
{
    public abstract IOrder CreateOrder(string orderId, DishType dishType, string dishName, decimal amount, DateTime orderTime);
}