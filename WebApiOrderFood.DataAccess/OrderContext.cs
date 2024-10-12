using Microsoft.EntityFrameworkCore;
using WebApiOrderFood.DataAccess.Entities;

namespace WebApiOrderFood.DataAccess;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options) : base(options) { }

    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<TransactionEntity> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderEntity>()
            .HasKey(o => o.OrderId);

        modelBuilder.Entity<OrderEntity>()
            .Property(o => o.OrderType)
            .IsRequired();

        modelBuilder.Entity<OrderEntity>()
            .Property(o => o.DishName)
            .IsRequired();

        modelBuilder.Entity<OrderEntity>()
            .Property(o => o.Amount)
            .IsRequired();

        modelBuilder.Entity<TransactionEntity>()
            .HasKey(t => t.TransactionId);

        modelBuilder.Entity<TransactionEntity>()
            .Property(t => t.OrderId)
            .IsRequired();

        modelBuilder.Entity<TransactionEntity>()
            .Property(t => t.TransactionType)
            .IsRequired();

        modelBuilder.Entity<TransactionEntity>()
            .Property(t => t.Amount)
            .IsRequired();
    }

    public async Task SeedDataAsync()
    {
        if (await Orders.AnyAsync() || await Transactions.AnyAsync()) return;

        var order1 = new OrderEntity
        {
            OrderId = "1",
            OrderType = OrderType.Delivery,
            DishType = DishType.FastFood,
            DishName = "Pizza",
            Amount = 2,
            OrderTime = DateTime.Now
        };

        var order2 = new OrderEntity
        {
            OrderId = "2",
            OrderType = OrderType.InTheEstablishment,
            DishType = DishType.First,
            DishName = "Solyanka",
            Amount = 1,
            OrderTime = DateTime.Now
        };

        var order3 = new OrderEntity
        {
            OrderId = "3",
            OrderType = OrderType.ForTakeaway,
            DishType = DishType.Drink,
            DishName = "Coffee",
            Amount = 3,
            OrderTime = DateTime.Now
        };

        await Orders.AddRangeAsync(order1, order2, order3);

        var transaction1 = new TransactionEntity
        {
            TransactionId = "1",
            OrderId = "1",
            TransactionType = TransactionType.Successfully,
            Amount = 1,
            DateTime = DateTime.Now
        };

        var transaction2 = new TransactionEntity
        {
            TransactionId = "2",
            OrderId = "2",
            TransactionType = TransactionType.Successfully,
            Amount = 1,
            DateTime = DateTime.Now
        };

        var transaction3 = new TransactionEntity
        {
            TransactionId = "3",
            OrderId = "3",
            TransactionType = TransactionType.Successfully,
            Amount = 1,
            DateTime = DateTime.Now
        };

        await Transactions.AddRangeAsync(transaction1, transaction2, transaction3);

        await SaveChangesAsync();
    }

}
