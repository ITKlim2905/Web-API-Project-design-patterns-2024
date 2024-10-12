using WebApiOrderFood.DataAccess.Entities;
using WebApiOrderFood.DataAccess.Repositories.Order;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Dtos;

namespace WebApiOrderFood.BusinessLogic.Facades;

public class OrderTransactionFacade
{
    private readonly ITransactionService _transactionService;
    private readonly IOrderRepository _orderRepository;

    public OrderTransactionFacade(ITransactionService transactionService, IOrderRepository orderRepository)
    {
        _transactionService = transactionService;
        _orderRepository = orderRepository;
    }

    public async Task CreateTransactionAndUpdateOrder(TransactionDto transaction)
    {
        var order = await _orderRepository.Get(transaction.OrderId);

        if (order == null)
            throw new ArgumentNullException($"Order not found by ID = {transaction.OrderId}");

        await _transactionService.Create(transaction);

        switch (transaction.Type)
        {
            case TransactionType.Successfully:
                order.Amount += transaction.Amount;
                break;
            case TransactionType.Unsuccessfully:
                order.Amount -= transaction.Amount;
                break;
        }

        await _orderRepository.Update(order);
    }

    public async Task RemoveTransactionAndUpdateOrder(string transactionId)
    {
        var transaction = await _transactionService.Get(transactionId);

        if (transaction == null || transaction == TransactionDto.Default)
            throw new ArgumentNullException($"Transaction not found by ID = {transactionId}");

        var order = await _orderRepository.Get(transaction.OrderId);

        if (order == null)
            throw new ArgumentNullException($"Order not found by ID = {transaction.OrderId}");

        await _transactionService.Remove(transactionId);

        switch (transaction.Type)
        {
            case TransactionType.Successfully:
                order.Amount -= transaction.Amount;
                break;
            case TransactionType.Unsuccessfully:
                order.Amount += transaction.Amount;
                break;
        }

        await _orderRepository.Update(order);
    }
}
