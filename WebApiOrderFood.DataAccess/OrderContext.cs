using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.DataAccess;

public class OrderContext
{
    public ICollection<OrderEntity> Orders { get; }
    public ICollection<TransactionEntity> Transactions { get; }

    public OrderContext()
    {
        Orders = new List<OrderEntity>();
        {
            new OrderEntity
            {
                OrderId = "1",
                OrderType = OrderType.Delivery,
                DishType = DishType.FastFood,
                DishName = "Pizza",
                Amount = 2,
                OrderTime = DateTime.Now
            };
            new OrderEntity
            {
                OrderId = "2",
                OrderType = OrderType.InTheEstablishment,
                DishType = DishType.First,
                DishName = "Solyanka",
                Amount = 1,
                OrderTime = DateTime.Now
            };
            new OrderEntity
            {
                OrderId = "3",
                OrderType = OrderType.ForTakeaway,
                DishType = DishType.Drink,
                DishName = "Coffee",
                Amount = 3,
                OrderTime = DateTime.Now
            };
        }

        Transactions = new List<TransactionEntity>();
        {
            new TransactionEntity
            {
                TransactionId = "1",
                OrderId = "1",
                TransactionType = TransactionType.Successfully,
                Amount = 1,
                DateTime = DateTime.Now
            };
            new TransactionEntity
            {
                TransactionId = "2",
                OrderId = "2",
                TransactionType = TransactionType.Successfully,
                Amount = 1,
                DateTime = DateTime.Now
            };
            new TransactionEntity
            {
                TransactionId = "3",
                OrderId = "3",
                TransactionType = TransactionType.Successfully,
                Amount = 1,
                DateTime = DateTime.Now
            };
        }
    }
}
