using System.Diagnostics;
using Microsoft.Extensions.Logging;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Dtos;

public class OrderServiceDecorator : IOrderService
{
    private readonly IOrderService _inner;
    private readonly ILogger<OrderServiceDecorator> _logger;

    public OrderServiceDecorator(IOrderService inner, ILogger<OrderServiceDecorator> logger)
    {
        _inner = inner;
        _logger = logger;
    }

    public async Task<IReadOnlyList<OrderDto>> Get()
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return await _inner.Get();
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Get orders executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public async Task<OrderDto> Get(string orderId)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            return await _inner.Get(orderId);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Get order by ID {orderId} executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public async Task Add(OrderDto order)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _inner.Add(order);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Add order executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public async Task Update(OrderDto order)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _inner.Update(order);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Update order executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }

    public async Task Remove(string orderId)
    {
        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _inner.Remove(orderId);
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation($"Remove order executed in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
