using WebApiOrderFoodClean.Domain.Entities;

namespace WebApiOrderFoodClean.Domain.Dtos;

public class TransactionDto : IEquatable<TransactionDto>
{
    public static readonly TransactionDto Default
        = new TransactionDto(string.Empty, string.Empty, TransactionType.Unsuccessfully, decimal.Zero, DateTime.MinValue);

    public string TransactionId { get; }
    public string OrderId { get; init; }
    public TransactionType Type { get; init; }
    public decimal Amount { get; }
    public DateTime OrderTime { get; }

    public TransactionDto(string transactionId, string orderId, TransactionType type, decimal amount, DateTime orderTime)
    {
        TransactionId = transactionId;
        OrderId = orderId;
        Type = type;
        Amount = amount;
        OrderTime = orderTime;
    }

    public bool Equals(TransactionDto? other)
    {
        if (other == null)
            return false;

        return TransactionId == other.TransactionId && OrderId == other.OrderId && Type == other.Type
            && Amount == other.Amount && OrderTime == other.OrderTime;

    }

    public override int GetHashCode()
    {
        return (TransactionId.GetHashCode() + OrderId.GetHashCode() + Type.GetHashCode()
            + Amount.GetHashCode() + OrderTime.GetHashCode()) * 45;
    }
}