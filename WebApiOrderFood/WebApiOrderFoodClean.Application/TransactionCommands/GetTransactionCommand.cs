using WebApiOrderFoodClean.Domain.Entities;
using WebApiOrderFoodClean.Infrastructure.Repositories.Order;

namespace WebApiOrderFoodClean.Application.TransactionCommands;

public class GetTransactionCommand
{
    private readonly ITransactionRepository _repository;

    public GetTransactionCommand(ITransactionRepository repository)
    {
        _repository = repository;
    }

    public Task<TransactionEntity> Execute(string info)
    {
        return _repository.Get(info)!;
    }
}
