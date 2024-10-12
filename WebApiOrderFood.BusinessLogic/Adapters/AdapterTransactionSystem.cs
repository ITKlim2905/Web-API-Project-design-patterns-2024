namespace WebApiOrderFood.BusinessLogic.Adapters;

public class AdapterTransactionSystem : IAdapterTransactionSystem
{
    public void ProcessAdapterTransaction(string adapterTransactionId, decimal adapterAmount, string adapterOrderId)
    {
        Console.WriteLine($"Adapter Transaction ID: {adapterTransactionId}, Amount: {adapterAmount}, Order ID: {adapterOrderId}");
    }
}
