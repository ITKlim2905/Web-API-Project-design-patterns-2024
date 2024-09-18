using System.ComponentModel.DataAnnotations;
using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.Models.Orders;

public class UpdateOrderRequest
{
    [Required]
    public string OrderId { get; init; }

    [Required]
    public OrderType OrderType { get; init; }
    public DishType DishType { get; init; }
    public string DishName { get; init; }

    [Required]
    public decimal Amount { get; init; }
}
