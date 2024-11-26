namespace WebApiOrderFoodClean.Adapters;

public class LegacyTransactionAdapter : IAdapterTransactionSystem
{
    private readonly LegacyTransactionSystem _legacySystem;
    private readonly ILogger<LegacyTransactionAdapter> _logger;

    public LegacyTransactionAdapter(LegacyTransactionSystem legacySystem, ILogger<LegacyTransactionAdapter> logger)
    {
        _legacySystem = legacySystem;
        _logger = logger;
    }

    public void ProcessAdapterTransaction(string transactionId, decimal amount, string orderId)
    {
        _legacySystem.PerformTransaction(transactionId, amount, orderId);
        _logger.LogInformation($"Processed transaction in Legacy System: ID = {transactionId}, Amount = {amount}, Order ID = {orderId}");
    }
}
