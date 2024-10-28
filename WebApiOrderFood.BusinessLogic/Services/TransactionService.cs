using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Dtos;
using WebApiOrderFood.BusinessLogic.Adapters;
using WebApiOrderFood.BusinessLogic.TransactionCommands;
using WebApiOrderFood.BusinessLogic.Enumerator;
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

    public TransactionService
        (
        ITransactionRepository transactionRepository,
        IOrderRepository orderRepository,
        IAdapterTransactionSystem adapterTransactionSystem,
        ILogger<TransactionService> logger
        )
    {
        _transactionRepository = transactionRepository;
        _orderRepository = orderRepository;
        _adapterTransactionSystem = adapterTransactionSystem;
        _logger = logger;
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