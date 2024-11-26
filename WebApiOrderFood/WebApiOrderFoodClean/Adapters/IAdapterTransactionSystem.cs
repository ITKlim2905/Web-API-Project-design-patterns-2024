namespace WebApiOrderFoodClean.Adapters;

public interface IAdapterTransactionSystem
{
    void ProcessAdapterTransaction(string adapterTransactionId, decimal adapterAmount, string adapterOrderId);
}
