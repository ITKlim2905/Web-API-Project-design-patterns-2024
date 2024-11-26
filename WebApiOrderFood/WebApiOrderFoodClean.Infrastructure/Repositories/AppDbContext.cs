using Microsoft.EntityFrameworkCore;
using WebApiOrderFoodClean.Domain.Entities;

namespace WebApiOrderFoodClean.Infrastructure.Repositories;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<OrderEntity> Order { get; set; }
}
