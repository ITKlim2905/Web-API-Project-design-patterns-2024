using Microsoft.AspNetCore.Mvc;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Dtos;
using WebApiOrderFood.DataAccess.Entities;
using WebApiOrderFood.Models.Transactions;

namespace WebApiOrderFood.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ILogger<TransactionController> _logger;
    private readonly ITransactionService _transactionService;

    public TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService)
    {
        _logger = logger;
        _transactionService = transactionService;
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
        try
        {

            await _transactionService.Create(entity);
            _logger.LogInformation($"Transaction {entity.TransactionId} successfully created");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to create transaction with Id = {entity.TransactionId}");
            return BadRequest();
        }
    }

    [HttpDelete]
    public async Task<ActionResult> Remove(string TransactionId)
    {
        try
        {
            await _transactionService.Remove(TransactionId);
            _logger.LogInformation($"Transaction {TransactionId} successfully deleted");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to delete transaction with ID = {TransactionId}");
            return BadRequest();
        }
    }
}