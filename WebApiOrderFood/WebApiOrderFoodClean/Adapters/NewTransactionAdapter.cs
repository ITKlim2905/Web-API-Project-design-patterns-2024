namespace WebApiOrderFoodClean.Adapters;

public class NewTransactionAdapter : IAdapterTransactionSystem
{
    private readonly NewTransactionSystem _newSystem;
    private readonly ILogger<NewTransactionAdapter> _logger;

    public NewTransactionAdapter(NewTransactionSystem newSystem, ILogger<NewTransactionAdapter> logger)
    {
        _newSystem = newSystem;
        _logger = logger;
    }

    public void ProcessAdapterTransaction(string transactionId, decimal amount, string orderId)
    {
        _newSystem.Execute(orderId, amount);
        _logger.LogInformation($"Processed transaction in New System: Order ID={orderId}, Amount={amount}");
    }
}
