using WebApiOrderFoodClean.Domain.Entities;

namespace WebApiOrderFoodClean.Domain.Factories;

public class DeliveryOrder : IOrder
{
    public string OrderId { get; private set; }
    public OrderType OrderType { get; private set; }
    public DishType DishType { get; private set; }
    public string DishName { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime OrderTime { get; private set; }

    public DeliveryOrder(string orderId, DishType dishType, string dishName, decimal amount)
    {
        OrderId = orderId;
        OrderType = OrderType.InTheEstablishment;
        DishType = dishType;
        DishName = dishName;
        Amount = amount;
        OrderTime = DateTime.UtcNow;
    }

    public void ProcessOrder()
    {
        Console.WriteLine($"Processing delivery order: order №{OrderId} - {DishName} for {Amount:C}");
    }
}

public class DeliveryOrderFactory : OrderFactory
{
    public override IOrder CreateOrder(string orderId, DishType dishType, string dishName, decimal amount, DateTime orderTime)
    {
        return new DeliveryOrder(orderId, dishType, dishName, amount);
    }
}