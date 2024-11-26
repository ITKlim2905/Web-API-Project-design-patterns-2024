using System.Collections;
using WebApiOrderFoodClean.Domain.Dtos;

namespace WebApiOrderFoodClean.Domain.Enumerator;

public class TransactionCollection : IEnumerable<TransactionDto>
{
    private List<TransactionDto> _transactions = new List<TransactionDto>();

    public void AddTransaction(TransactionDto transaction)
    {
        _transactions.Add(transaction);
    }

    public IEnumerator<TransactionDto> GetEnumerator()
    {
        return new TransactionEnumerator(_transactions);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
