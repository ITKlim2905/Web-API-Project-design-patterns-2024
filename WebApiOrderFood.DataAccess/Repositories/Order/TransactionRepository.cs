using System.Collections.ObjectModel;
using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.DataAccess.Repositories.Order;

public class TransactionRepository : ITransactionRepository
{
    private readonly OrderContext _context;

    public TransactionRepository(OrderContext context)
    {
        _context = context;
    }

    public Task<ReadOnlyCollection<TransactionEntity>> Get() =>
        Task.FromResult(_context.Transactions.ToList().AsReadOnly());

    public Task<TransactionEntity?> Get(string TransactionID) =>
        Task.FromResult(_context.Transactions.FirstOrDefault(e => e.TransactionID == TransactionID));

    public Task<ReadOnlyCollection<TransactionEntity>> Get(Func<TransactionEntity, bool> predicate)
    {
        return Task.FromResult(_context.Transactions.Where(predicate).ToList().AsReadOnly());
    }

    public Task Create(TransactionEntity entity)
    {
        _context.Transactions.Add(entity);
        return Task.CompletedTask;
    }

    public Task Update(TransactionEntity entity)
    {
        foreach (var e in _context.Transactions)
        {
            if (e.TransactionID == entity.TransactionID)
            {
                e.OrderID = entity.OrderID;
                e.TransactionType = entity.TransactionType;
                e.Amount = entity.Amount;
                e.DateTime = entity.DateTime;
            }
        }
        return Task.CompletedTask;
    }

    public Task Delete(string TransactionID)
    {
        var entity = _context.Transactions.FirstOrDefault(e => e.TransactionID == TransactionID);
        if (entity != null)
        {
            _context.Transactions.Remove(entity);
        }
        return Task.CompletedTask;
    }
}
