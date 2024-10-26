namespace WebApiOrderFood.BusinessLogic.Adapters;

public class LegacyTransactionSystem
{
    public void PerformTransaction(string transactionId, decimal amount, string orderId)
    {
        if (string.IsNullOrEmpty(transactionId) || amount <= 0)
        {
            throw new ArgumentException("Invalid transaction parameters.");
        }
    }
}
