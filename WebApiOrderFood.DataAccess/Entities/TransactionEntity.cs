namespace WebApiOrderFood.DataAccess.Entities;

public class TransactionEntity
{
    public string TransactionID { get; init; }
    public string OrderID { get; set; }
    public TransactionType TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
}
