namespace WebApiOrderFood.BusinessLogic.Adapters;

public class NewTransactionSystem
{
    public void Execute(string orderId, decimal amount)
    {
        if (string.IsNullOrEmpty(orderId) || amount <= 0)
        {
            throw new ArgumentException("Invalid order parameters.");
        }
    }
}
