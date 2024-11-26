using Microsoft.AspNetCore.Mvc;
using WebApiOrderFoodClean.Domain.Contracts;
using WebApiOrderFoodClean.Domain.Dtos;
using WebApiOrderFoodClean.Models.Orders;

namespace WebApiOrderFoodClean.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;
    private readonly IOrderQueryService _orderQueryService;
    private readonly IOrderCommandService _orderCommandService;

    public OrderController
    (
        ILogger<OrderController> logger,
        IOrderService orderService,
        IOrderQueryService orderQueryService,
        IOrderCommandService orderCommandService
    )
    {
        _logger = logger;
        _orderService = orderService;
        _orderQueryService = orderQueryService;
        _orderCommandService = orderCommandService;
    }

    [HttpGet("get", Name = "GetOrder")]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
    {
        try
        {
            var result = await _orderQueryService.Get();
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch orders");
            return BadRequest("Error occurred while fetching orders.");
        }
    }

    [HttpGet("getById/{OrderId}", Name = "GetOrderById")]
    public async Task<ActionResult<OrderDto>> GetOrderById([FromRoute] string orderId)
    {
        try
        {
            var result = await _orderQueryService.Get(orderId);
            if (result == null)
                return NotFound($"Order with ID {orderId} not found.");

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to fetch order with ID = {orderId}");
            return BadRequest("Error occurred while fetching the order.");
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddOrder([FromBody] CreateOrderRequest request)
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
            await _orderCommandService.Create(entity);
            _logger.LogInformation($"Order {entity.OrderId} successfully created.");
            return Ok($"Order {entity.OrderId} successfully created.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to create order with ID = {entity.OrderId}");
            return BadRequest("Error occurred while creating the order.");
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateOrder([FromBody] UpdateOrderRequest request)
    {
        var entity = new OrderDto(
            orderId: request.OrderId,
            orderType: request.OrderType,
            dishType: request.DishType,
            dishName: request.DishName,
            amount: request.Amount,
            orderTime: DateTime.UtcNow);

        try
        {
            await _orderCommandService.Update(entity);
            _logger.LogInformation($"Order {entity.OrderId} successfully updated.");
            return Ok($"Order {entity.OrderId} successfully updated.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to update order with ID = {entity.OrderId}");
            return BadRequest("Error occurred while updating the order.");
        }
    }

    [HttpDelete("{orderId}")]
    public async Task<ActionResult> RemoveOrder([FromRoute] string orderId)
    {
        try
        {
            await _orderCommandService.Delete(orderId);
            _logger.LogInformation($"Order {orderId} successfully deleted.");
            return Ok($"Order {orderId} successfully deleted.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to delete order with ID = {orderId}");
            return BadRequest("Error occurred while deleting the order.");
        }
    }
}