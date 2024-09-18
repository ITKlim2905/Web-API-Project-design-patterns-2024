using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.BusinessLogic.Dtos;

public class OrderDto : IEquatable<OrderDto>
{
    public static readonly OrderDto Default
        = new OrderDto(string.Empty, OrderType.InTheEstablishment, DishType.First, string.Empty, decimal.Zero, DateTime.MinValue);

    public string OrderID { get; }
    public OrderType OrderType { get; }
    public DishType DishType { get; }
    public string DishName { get; }
    public decimal Amount { get; }
    public DateTime OrderTime { get; }

    public OrderDto(string orderID, OrderType orderType, DishType dishType, string dishName, decimal amount, DateTime orderTime)
    {
        OrderID = orderID;
        OrderType = orderType;
        DishType = dishType;
        DishName = dishName;
        Amount = amount;
        OrderTime = orderTime;
    }

    public bool Equals(OrderDto? other)
    {
        if (other == null)
            return false;

        return OrderID == other.OrderID && OrderType == other.OrderType && DishType == other.DishType
            && DishName == other.DishName && Amount == other.Amount && OrderTime == other.OrderTime;
    }

    public override int GetHashCode()
    {
        return (OrderID.GetHashCode() + OrderType.GetHashCode() + DishType.GetHashCode()
            + DishName.GetHashCode() + Amount.GetHashCode() + OrderTime.GetHashCode()) * 45;
    }
}
