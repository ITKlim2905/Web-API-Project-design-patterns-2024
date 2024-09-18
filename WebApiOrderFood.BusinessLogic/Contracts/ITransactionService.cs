﻿using WebApiOrderFood.BusinessLogic.Dtos;

namespace WebApiOrderFood.BusinessLogic.Contracts;

public interface ITransactionService
{
    Task<IReadOnlyList<TransactionDto>> Get();
    Task<TransactionDto> Get(string transactionIS);
    Task<IReadOnlyList<TransactionDto>> GetTodayTransactions();
    Task Create(TransactionDto transaction);
    Task Remove(string transactionID);
}