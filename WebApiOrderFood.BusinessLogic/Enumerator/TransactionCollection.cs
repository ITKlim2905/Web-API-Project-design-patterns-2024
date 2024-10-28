using System.Collections;
using WebApiOrderFood.BusinessLogic.Dtos;

namespace WebApiOrderFood.BusinessLogic.Enumerator;

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
