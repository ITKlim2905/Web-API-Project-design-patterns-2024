using WebApiOrderFood.BusinessLogic.Dtos;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Services;
using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.BusinessLogic.Mediator;

public class Mediator : IMediator
{
    private readonly Lazy<IOrderService> _orderService;
    private readonly Lazy<ITransactionService> _transactionService;

    public Mediator(Lazy<IOrderService> orderService, Lazy<ITransactionService> transactionService)
    {
        _orderService = orderService;
        _transactionService = transactionService;
    }

    /* public async Task Notify(object sender, string eventCode, object data = null)
    {
        if (eventCode == "OrderCreated" && sender is OrderService)
        {
            var orderData = data as OrderDto;
            if (orderData != null)
            {
                await _transactionService.ProcessTransaction(orderData.OrderId, orderData.Amount, TransactionType.Successfully);
            }
        }
    } */
    public async Task Notify(object sender, string eventCode, object data = null)
    {
        if (eventCode == "OrderCreated" && sender is IOrderService)
        {
            var orderDto = data as OrderDto;
            if (orderDto != null)
            {
                await _transactionService.Value.Create(new TransactionDto(
                    Guid.NewGuid().ToString(),
                    orderDto.OrderId,
                    TransactionType.Successfully,
                    orderDto.Amount,
                    DateTime.UtcNow));
            }
        }
        else if (eventCode == "TransactionCreated" && sender is ITransactionService)
        {
            var transactionDto = data as TransactionDto;
            if (transactionDto != null)
            {
                await _orderService.Value.UpdateOrderAmount(transactionDto.OrderId, transactionDto.Amount, transactionDto.Type);
            }
        }
    }
}
