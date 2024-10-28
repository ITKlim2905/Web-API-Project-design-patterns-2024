using System.Collections.ObjectModel;
using WebApiOrderFood.BusinessLogic.Commands;
using WebApiOrderFood.DataAccess.Entities;
using WebApiOrderFood.DataAccess.Repositories.Order;

namespace WebApiOrderFood.BusinessLogic.TransactionCommands;

public class GetTransactionsCommand : IAsyncCommand<ReadOnlyCollection<TransactionEntity>>
{
    private readonly ITransactionRepository _repository;

    public GetTransactionsCommand(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public Task<ReadOnlyCollection<TransactionEntity>> Execute()
    {
        return _repository.Get();
    }
}
