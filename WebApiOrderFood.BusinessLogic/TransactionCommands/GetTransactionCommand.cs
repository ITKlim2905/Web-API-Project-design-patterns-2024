using WebApiOrderFood.DataAccess.Entities;
using WebApiOrderFood.DataAccess.Repositories.Order;

namespace WebApiOrderFood.BusinessLogic.TransactionCommands;

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
