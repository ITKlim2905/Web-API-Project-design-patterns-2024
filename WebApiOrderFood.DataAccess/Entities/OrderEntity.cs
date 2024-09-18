namespace WebApiOrderFood.DataAccess.Entities;

public class OrderEntity
{
    public string OrderId { get; init; }
    public OrderType OrderType { get; set; }
    public DishType DishType { get; set; }
    public string DishName { get; set; }
    public decimal Amount { get; set; }
    public DateTime OrderTime { get; set; }
}
