using System.Diagnostics;
using Microsoft.Extensions.Logging;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Dtos;

public class TransactionServiceDecorator : ITransactionService
{
    private readonly ITransactionService _inner;
    private readonly ILogger<TransactionServiceDecorator> _logger;

    public TransactionServiceDecorator(ITransactionService inner, ILogger<TransactionServiceDecorator> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<IReadOnlyList<TransactionDto>> Get()
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return await _inner.Get();
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Get transactions executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public async Task<TransactionDto> Get(string transactionId)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return await _inner.Get(transactionId);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Get transaction by ID {transactionId} executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public async Task<IReadOnlyList<TransactionDto>> GetTodayTransactions()
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return await _inner.GetTodayTransactions();
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Get today's transactions executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public async Task Create(TransactionDto transaction)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _inner.Create(transaction);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Create transaction executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public async Task Remove(string transactionId)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _inner.Remove(transactionId);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Remove transaction executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}