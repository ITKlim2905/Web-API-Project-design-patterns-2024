using WebApiOrderFoodClean.Domain.Entities;
using WebApiOrderFoodClean.Domain.Dtos;

namespace WebApiOrderFoodClean.Domain.Contracts;

public interface IOrderService
{
    Task<IReadOnlyList<OrderDto>> Get();
    Task<OrderDto> Get(string orderId);
    Task Add(OrderDto order);
    Task Update(OrderDto order);
    Task Remove(string orderId);
    Task UpdateOrderAmount(string orderId, decimal amount, TransactionType transactionType);

}
