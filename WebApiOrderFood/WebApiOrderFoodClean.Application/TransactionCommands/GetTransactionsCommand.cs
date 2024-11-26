using System.Collections.ObjectModel;
using WebApiOrderFoodClean.Application.Commands;
using WebApiOrderFoodClean.Domain.Entities;
using WebApiOrderFoodClean.Infrastructure.Repositories.Order;

namespace WebApiOrderFoodClean.Application.TransactionCommands;

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
