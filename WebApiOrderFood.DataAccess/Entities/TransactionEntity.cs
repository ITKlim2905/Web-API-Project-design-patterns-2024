namespace WebApiOrderFood.DataAccess.Entities;

public class TransactionEntity
{
    public string TransactionId { get; init; }
    public string OrderId { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
}
