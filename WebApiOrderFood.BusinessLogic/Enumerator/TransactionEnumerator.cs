using System.Collections;
using WebApiOrderFood.BusinessLogic.Dtos;

namespace WebApiOrderFood.BusinessLogic.Enumerator;

public class TransactionEnumerator : IEnumerator<TransactionDto>
{
    private readonly List<TransactionDto> _transactions;
    private int _position = -1;

    public TransactionEnumerator(List<TransactionDto> transactions)
    {
        _transactions = transactions;
    }

    public TransactionDto Current => _transactions[_position];

    object IEnumerator.Current => Current;

    public bool MoveNext()
    {
        _position++;
        return _position < _transactions.Count;
    }

    public void Reset()
    {
        _position = -1;
    }

    public void Dispose() { }
}
