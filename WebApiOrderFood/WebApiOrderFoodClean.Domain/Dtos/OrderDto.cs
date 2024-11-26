using WebApiOrderFoodClean.Domain.Entities;

namespace WebApiOrderFoodClean.Domain.Dtos;

public class OrderDto : IEquatable<OrderDto>, ICloneable
{
    public static readonly OrderDto Default
        = new OrderDto(string.Empty, OrderType.InTheEstablishment, DishType.First, string.Empty, decimal.Zero, DateTime.MinValue);

    public string OrderId { get; set; }
    public OrderType OrderType { get; set; }
    public DishType DishType { get; set; }
    public string DishName { get; set; }
    public decimal Amount { get; set; }

    public DateTime OrderTime { get; set; }

    public OrderDto(string orderId, OrderType orderType, DishType dishType, string dishName, decimal amount, DateTime orderTime)
    {
        OrderId = orderId;
        OrderType = orderType;
        DishType = dishType;
        DishName = dishName;
        Amount = amount;
        OrderTime = orderTime;
    }

    public void UpdateOrder(decimal amount) =>
        Amount += amount;

    public OrderDto Clone()
    {
        return new OrderDto(
            this.OrderId,
            this.OrderType,
            this.DishType,
            this.DishName,
            this.Amount,
            this.OrderTime
        );
    }

    object ICloneable.Clone()
    {
        return Clone();
    }

    public bool Equals(OrderDto? other)
    {
        if (other == null)
            return false;

        return OrderId == other.OrderId && OrderType == other.OrderType && DishType == other.DishType
            && DishName == other.DishName && Amount == other.Amount && OrderTime == other.OrderTime;
    }

    public override int GetHashCode()
    {
        return (OrderId.GetHashCode() + OrderType.GetHashCode() + DishType.GetHashCode()
            + DishName.GetHashCode() + Amount.GetHashCode() + OrderTime.GetHashCode()) * 45;
    }
}
