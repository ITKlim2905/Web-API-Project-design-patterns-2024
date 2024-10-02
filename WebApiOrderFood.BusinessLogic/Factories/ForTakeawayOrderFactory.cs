using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.BusinessLogic.Factories;

public class ForTakeawayOrder : IOrder
{
    public string OrderId { get; private set; }
    public OrderType OrderType { get; private set; }
    public DishType DishType { get; private set; }
    public string DishName { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime OrderTime { get; private set; }

    public ForTakeawayOrder(string orderId, DishType dishType, string dishName, decimal amount)
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
        Console.WriteLine($"Processing for takeaway order: order №{OrderId} - {DishName} for {Amount:C}");
    }
}

public class ForTakeawayOrderFactory : OrderFactory
{
    public override IOrder CreateOrder(string orderId, DishType dishType, string dishName, decimal amount, DateTime orderTime)
    {
        return new ForTakeawayOrder(orderId, dishType, dishName, amount);
    }
}