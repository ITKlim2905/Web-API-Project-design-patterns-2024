using System.Collections.ObjectModel;
using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.DataAccess.Repositories.Order;

public interface ITransactionRepository
{
    public Task<ReadOnlyCollection<TransactionEntity>> Get();
    public Task<TransactionEntity?> Get(string TransactionID);
    public Task<ReadOnlyCollection<TransactionEntity>> Get(Func<TransactionEntity, bool> predicate);
    public Task Create(TransactionEntity entity);
    public Task Update(TransactionEntity entity);
    public Task Delete(string TransactionID);
}
