using WebApiOrderFoodClean.Domain.Dtos;

namespace WebApiOrderFoodClean.Domain.Contracts;

public interface IOrderCommandService
{
    Task Create(OrderDto order);
    Task Update(OrderDto order);
    Task Delete(string orderId);
}
