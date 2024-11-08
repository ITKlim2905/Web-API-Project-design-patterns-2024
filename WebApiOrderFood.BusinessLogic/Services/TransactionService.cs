using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Dtos;
using WebApiOrderFood.BusinessLogic.Adapters;
using WebApiOrderFood.BusinessLogic.TransactionCommands;
using WebApiOrderFood.BusinessLogic.Enumerator;
using WebApiOrderFood.BusinessLogic.Mediator;
using WebApiOrderFood.DataAccess.Entities;
using WebApiOrderFood.DataAccess.Repositories.Order;
using Microsoft.Extensions.Logging;

namespace WebApiOrderFood.BusinessLogic.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IAdapterTransactionSystem _adapterTransactionSystem;
    private readonly ILogger<TransactionService> _logger;
    private readonly IMediator _mediator;

    public TransactionService
        (
        ITransactionRepository transactionRepository,
        IOrderRepository orderRepository,
        IAdapterTransactionSystem adapterTransactionSystem,
        ILogger<TransactionService> logger,
        IMediator mediator
        )
    {
        _transactionRepository = transactionRepository;
        _orderRepository = orderRepository;
        _adapterTransactionSystem = adapterTransactionSystem;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task ProcessTransaction(string orderId, decimal amount, TransactionType transactionType)
    {
        var order = await _orderRepository.Get(orderId);
        if (order == null)
        {
            _logger.LogError($"Order not found by id={orderId}");
            return;
        }

        var transaction = new TransactionEntity
        {
            TransactionId = Guid.NewGuid().ToString(),
            OrderId = orderId,
            TransactionType = transactionType,
            Amount = amount,
            DateTime = DateTime.UtcNow
        };

        await _transactionRepository.Create(transaction);
        _adapterTransactionSystem.ProcessAdapterTransaction(transaction.TransactionId, transaction.Amount, transaction.OrderId);

        // Обновляем баланс заказа
        switch (transactionType)
        {
            case TransactionType.Successfully:
                order.Amount += amount;
                break;
            case TransactionType.Unsuccessfully:
                order.Amount -= amount;
                break;
        }

        await _orderRepository.Update(order);
        _logger.LogInformation($"Transaction processed for order {orderId} with amount {amount}");
        await _mediator.Notify(this, "TransactionCreated", transaction);
    }

    public async Task<IReadOnlyList<TransactionDto>> Get()
    {
        // var transactions = await _transactionRepository.Get();
        var command = new GetTransactionsCommand(_transactionRepository);
        var transactions = await command.Execute();
        if (transactions == null)
            return new List<TransactionDto>();

        return transactions.Select(e => new TransactionDto(
                e.TransactionId, e.OrderId, e.TransactionType, e.Amount, e.DateTime)).ToList().AsReadOnly();
    }

    public async Task<TransactionDto> Get(string transactionId)
    {
        var transaction = await _transactionRepository.Get(transactionId);
        if (transaction == null)
            return TransactionDto.Default;

        return new TransactionDto(transaction.TransactionId, transaction.OrderId,
            transaction.TransactionType, transaction.Amount, transaction.DateTime);
    }

    public async Task<IReadOnlyList<TransactionDto>> GetTodayTransactions()
    {
        var transactions = await _transactionRepository.Get(t
            => t.DateTime.Date == DateTime.UtcNow.Date);
        if (transactions == null)
            return new List<TransactionDto>();

        return transactions.Select(e => new TransactionDto(
            e.TransactionId, e.OrderId, e.TransactionType, e.Amount, e.DateTime)).ToList().AsReadOnly();
    }

    public async Task Create(TransactionDto transaction)
    {
        if (transaction != TransactionDto.Default)
        {
            var order = await _orderRepository.Get(transaction.OrderId);

            if (order == null)
                throw new ArgumentNullException($"Account not found by id={transaction.OrderId}");

            await _transactionRepository.Create(new TransactionEntity
            {
                TransactionId = transaction.TransactionId,
                OrderId = transaction.OrderId,
                TransactionType = transaction.Type,
                Amount = transaction.Amount,
                DateTime = transaction.OrderTime
            });

            var transactionEntity = new TransactionEntity
            {
                TransactionId = transaction.TransactionId,
                OrderId = transaction.OrderId,
                TransactionType = transaction.Type,
                Amount = transaction.Amount,
                DateTime = transaction.OrderTime
            };

            _adapterTransactionSystem.ProcessAdapterTransaction(transactionEntity.TransactionId, transactionEntity.Amount, transactionEntity.OrderId);

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
    }

    public async Task Remove(string transactionId)
    {
        if (string.IsNullOrEmpty(transactionId))
            throw new ArgumentNullException();

        await _transactionRepository.Delete(transactionId);
    }

    public async Task Iterator()
    {
        TransactionCollection transactions = new TransactionCollection();

        foreach (var transaction in transactions)
        {
            _logger.LogInformation("{TransactionId}: {Amount}", transaction.TransactionId, transaction.Amount);
        }
    }
}