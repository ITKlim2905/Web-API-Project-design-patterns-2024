using WebApiOrderFoodClean.Domain.Dtos;
using WebApiOrderFoodClean.Domain.Entities;

namespace WebApiOrderFoodClean.Infrastructure.Mapper;

public static class OrderMapper
{
    public static OrderEntity ToEntity(this OrderDto dto)
    {
        return new OrderEntity
        {
            OrderId = dto.OrderId,
            OrderType = dto.OrderType,
            DishType = dto.DishType,
            DishName = dto.DishName,
            Amount = dto.Amount,
            OrderTime = dto.OrderTime
        };
    }

    public static OrderDto ToDto(this OrderEntity entity)
    {
        return new OrderDto(
            orderId: entity.OrderId,
            orderType: entity.OrderType,
            dishType: entity.DishType,
            dishName: entity.DishName,
            amount: entity.Amount,
            orderTime: entity.OrderTime
        );
    }

}
