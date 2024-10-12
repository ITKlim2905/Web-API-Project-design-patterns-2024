using WebApiOrderFood.DataAccess;
using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.BusinessLogic.Adapters
{
    public class TransactionAdapter : IAdapterTransactionSystem
    {
        private readonly OrderContext _context;

        public TransactionAdapter(OrderContext context)
        {
            _context = context;
        }

        public void ProcessAdapterTransaction(string adapterTransactionId, decimal adapterAmount, string adapterOrderId)
        {
            var transaction = new TransactionEntity
            {
                TransactionId = adapterTransactionId,
                Amount = adapterAmount,
                OrderId = adapterOrderId,
                TransactionType = TransactionType.Successfully,
                DateTime = DateTime.Now
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            Console.WriteLine($"Processed adapted transaction: {adapterTransactionId}, Amount: {adapterAmount}, Order ID: {adapterOrderId}");
        }
    }
}

