using System.ComponentModel.DataAnnotations;
using WebApiOrderFoodClean.Domain.Entities;

namespace WebApiOrderFoodClean.Models.Orders;

public class CreateOrderRequest
{
    [Required]
    public OrderType OrderType { get; init; }
    public DishType DishType { get; init; }
    public string DishName { get; init; }

    [Required]
    public decimal Amount { get; init; }
}
