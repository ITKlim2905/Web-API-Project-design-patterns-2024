using Microsoft.AspNetCore.Mvc;
using WebApiOrderFoodClean.Domain.Contracts;
using WebApiOrderFoodClean.Domain.Dtos;
using WebApiOrderFoodClean.Application.Facades;
using WebApiOrderFoodClean.Adapters;
using WebApiOrderFoodClean.Models.Transactions;

namespace WebApiOrderFoodClean.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ILogger<TransactionController> _logger;
    private readonly ITransactionService _transactionService;
    private readonly OrderTransactionFacade _orderTransactionFacade;
    private readonly IAdapterTransactionSystem _adapterTransactionSystem;

    public TransactionController
    (
        ILogger<TransactionController> logger,
        ITransactionService transactionService,
        OrderTransactionFacade orderTransactionFacade,
        IAdapterTransactionSystem adapterTransactionSystem
    )
    {
        _logger = logger;
        _transactionService = transactionService;
        _orderTransactionFacade = orderTransactionFacade;
    }

    [HttpGet("get", Name = "GetTransaction")]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTransaction()
    {
        try
        {
            var result = await _transactionService.Get();
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to fetch transaction");
            return BadRequest();
        }
    }

    [HttpGet("getById/{TransactionId}", Name = "GetTransactionById")]
    public async Task<ActionResult<TransactionDto>> GetTransactionById([FromRoute] string TransactionId)
    {
        try
        {
            var result = await _transactionService.Get(TransactionId);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to fetch transaction");
            return BadRequest();
        }
    }

    [HttpGet("getTodayTransaction", Name = "GetTodayTransaction")]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetTodayTransactions()
    {
        try
        {
            var result = await _transactionService.GetTodayTransactions();
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to fetch transactions");
            return BadRequest();
        }
    }

    [HttpPost("addTransaction", Name = "AddTransaction")]
    public async Task<ActionResult> Add([FromBody] CreateTransactionRequest request)
    {
        var entity = new TransactionDto(Guid.NewGuid().ToString(), request.OrderId,
            request.TransactionType, request.Amount, DateTime.UtcNow);

        var transactionDto = new TransactionDto(
        Guid.NewGuid().ToString(),
        request.OrderId,
        request.TransactionType,
        request.Amount,
        DateTime.UtcNow
        );

        try
        {
            await _orderTransactionFacade.CreateTransactionAndUpdateOrder(transactionDto);
            _logger.LogInformation($"Transaction {transactionDto.TransactionId} successfully created and order updated");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to create transaction with Id = {entity.TransactionId}");
            return BadRequest();
        }
    }

    [HttpPost("process")]
    public IActionResult ProcessTransaction([FromBody] CreateTransactionRequest request)
    {
        _adapterTransactionSystem.ProcessAdapterTransaction(request.TransactionId, request.Amount, request.OrderId);
        return Ok("Transaction processed successfully.");
    }

    [HttpDelete]
    public async Task<ActionResult> Remove(string TransactionId)
    {
        try
        {
            await _orderTransactionFacade.RemoveTransactionAndUpdateOrder(TransactionId);
            _logger.LogInformation($"Transaction {TransactionId} successfully deleted and order updated");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to delete transaction with ID = {TransactionId}");
            return BadRequest();
        }
    }
}