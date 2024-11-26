using WebApiOrderFoodClean.Domain.Dtos;

namespace WebApiOrderFoodClean.Domain.Contracts;

public interface IOrderQueryService
{
    Task<IEnumerable<OrderDto>> Get();
    Task<OrderDto> Get(string orderId);
}