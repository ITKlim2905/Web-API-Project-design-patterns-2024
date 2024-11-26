using System.ComponentModel.DataAnnotations;
using WebApiOrderFoodClean.Domain.Entities;

namespace WebApiOrderFoodClean.Models.Transactions;

public class CreateTransactionRequest
{
    [Required]
    public string OrderId { get; init; }

    [Required]
    public string TransactionId { get; init; }

    [Required]
    public TransactionType TransactionType { get; init; }

    [Required]
    public decimal Amount { get; init; }
}
