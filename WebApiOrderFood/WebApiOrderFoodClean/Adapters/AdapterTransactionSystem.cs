namespace WebApiOrderFoodClean.Adapters;

public class AdapterTransactionSystem : IAdapterTransactionSystem
{
    private readonly ILogger<AdapterTransactionSystem> _logger;

    public AdapterTransactionSystem(ILogger<AdapterTransactionSystem> logger)
    {
        _logger = logger;
    }

    public void ProcessAdapterTransaction(string adapterTransactionId, decimal adapterAmount, string adapterOrderId)
    {
        _logger.LogInformation($"Adapter Transaction ID: {adapterTransactionId}, Amount: {adapterAmount}, Order ID: {adapterOrderId}");
    }
}
