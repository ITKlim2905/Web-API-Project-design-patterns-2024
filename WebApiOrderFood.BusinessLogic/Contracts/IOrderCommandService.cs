using WebApiOrderFood.BusinessLogic.Dtos;

namespace WebApiOrderFood.BusinessLogic.Contracts;

public interface IOrderCommandService
{
    Task Create(OrderDto order);
    Task Update(OrderDto order);
    Task Delete(string orderId);
}
