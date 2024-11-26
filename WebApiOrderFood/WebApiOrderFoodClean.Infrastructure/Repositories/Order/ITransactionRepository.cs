using System.Collections.ObjectModel;
using WebApiOrderFoodClean.Domain.Entities;

namespace WebApiOrderFoodClean.Infrastructure.Repositories.Order;

public interface ITransactionRepository
{
    public Task<ReadOnlyCollection<TransactionEntity>> Get();
    public Task<TransactionEntity?> Get(string TransactionId);
    public Task<ReadOnlyCollection<TransactionEntity>> Get(Func<TransactionEntity, bool> predicate);
    public Task Create(TransactionEntity entity);
    public Task Update(TransactionEntity entity);
    public Task Delete(string TransactionId);
}
