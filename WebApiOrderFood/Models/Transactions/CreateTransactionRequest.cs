using System.ComponentModel.DataAnnotations;
using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.Models.Transactions;

public class CreateTransactionRequest
{
    [Required]
    public string OrderId { get; init; }

    [Required]
    public TransactionType TransactionType { get; init; }

    [Required]
    public decimal Amount { get; init; }
}
