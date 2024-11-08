using WebApiOrderFood.BusinessLogic.Dtos;

namespace WebApiOrderFood.BusinessLogic.Contracts;

public interface IOrderQueryService
{
    Task<IReadOnlyList<OrderDto>> Get();
    Task<OrderDto> Get(string orderId);
}