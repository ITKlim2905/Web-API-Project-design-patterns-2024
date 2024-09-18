using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.BusinessLogic.Dtos;

public class TransactionDto : IEquatable<TransactionDto>
{
    public static readonly TransactionDto Default
        = new TransactionDto(string.Empty, string.Empty, TransactionType.Unsuccessfully, decimal.Zero, DateTime.MinValue);

    public string TransactionID { get; }
    public string OrderID { get; init; }
    public TransactionType Type { get; init; }
    public decimal Amount { get; }
    public DateTime OrderTime { get; }

    public TransactionDto(string transactionID, string orderID, TransactionType type, decimal amount, DateTime orderTime)
    {
        TransactionID = transactionID;
        OrderID = orderID;
        Type = type;
        Amount = amount;
        OrderTime = orderTime;
    }

    public bool Equals(TransactionDto? other)
    {
        if (other == null)
            return false;

        return TransactionID == other.TransactionID && OrderID == other.OrderID && Type == other.Type
            && Amount == other.Amount && OrderTime == other.OrderTime;

    }

    public override int GetHashCode()
    {
        return (TransactionID.GetHashCode() + OrderID.GetHashCode() + Type.GetHashCode()
            + Amount.GetHashCode() + OrderTime.GetHashCode()) * 45;
    }
}