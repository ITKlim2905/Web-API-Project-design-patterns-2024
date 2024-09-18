using Microsoft.AspNetCore.Mvc;
using WebApiOrderFood.BusinessLogic.Contracts;
using WebApiOrderFood.BusinessLogic.Dtos;
using WebApiOrderFood.Models.Orders;

namespace WebApiOrderFood.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<TransactionController> _logger;
    private readonly IOrderService _orderService;

    public OrderController(ILogger<TransactionController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    [HttpGet("get", Name = "GetOrder")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrder()
    {
        try
        {
            var result = await _orderService.Get();
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to fetch orders");
            return BadRequest();
        }
    }

    [HttpGet("getById/{OrderId}", Name = "GetOrderById")]
    public async Task<ActionResult<TransactionDto>> GetOrderById([FromRoute] string OrderId)
    {
        try
        {
            var result = await _orderService.Get(OrderId);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Failed to fetch orders");
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] CreateOrderRequest request)
    {
        var entity = new OrderDto(
            orderId: Guid.NewGuid().ToString(),
            orderType: request.OrderType,
            dishType: request.DishType,
            dishName: request.DishName,
            amount: request.Amount,
            orderTime: DateTime.UtcNow);
        try
        {
            await _orderService.Add(entity);
            _logger.LogInformation($"Order {entity.OrderId} successfully created");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to create order with ID = {entity.OrderId}");
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] UpdateOrderRequest request)
    {
        var entity = new OrderDto(
            orderId: Guid.NewGuid().ToString(),
            orderType: request.OrderType,
            dishType: request.DishType,
            dishName: request.DishName,
            amount: request.Amount,
            orderTime: DateTime.UtcNow);

        try
        {
            await _orderService.Update(entity);
            _logger.LogInformation($"Order {entity.OrderId} successfully updated");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to update order with ID = {entity.OrderId}");
            return BadRequest();
        }
    }

    [HttpDelete]
    public async Task<ActionResult> Remove(string OrderId)
    {
        try
        {
            await _orderService.Remove(OrderId);
            _logger.LogInformation($"Order {OrderId} successfully deleted");
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to delete order with ID = {OrderId}");
            return BadRequest();
        }
    }
}