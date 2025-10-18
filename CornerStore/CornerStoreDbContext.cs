using Microsoft.EntityFrameworkCore;
using CornerStore.Models;
using System.Security.Cryptography.X509Certificates;
public class CornerStoreDbContext : DbContext
{

    public DbSet<Cashier> Cashiers { get; set; }
    public DbSet<Category> Categorys { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderProduct> OrderProducts { get; set; }

    public DbSet<Product> Products { get; set; }


    public CornerStoreDbContext(DbContextOptions<CornerStoreDbContext> context) : base(context)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cashier>().HasData(
            new Cashier { Id = 1, FirstName = "Alice", LastName = "Johnson" },
            new Cashier { Id = 2, FirstName = "Bob", LastName = "Miller" }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, CategoryName = "Beverages" },
            new Category { Id = 2, CategoryName = "Snacks" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, ProductName = "Cola", Price = 1.50m, brand = "FizzCo", CategoryId = 1 },
            new Product { Id = 2, ProductName = "Chips", Price = 2.00m, brand = "CrunchTime", CategoryId = 2 },
            new Product { Id = 3, ProductName = "Water", Price = 1.00m, brand = "ClearSpring", CategoryId = 1 }
        );

        modelBuilder.Entity<Order>().HasData(
            new Order { Id = 1, CashierId = 1, PaidOnDate = DateTime.UtcNow },
            new Order { Id = 2, CashierId = 2, PaidOnDate = DateTime.UtcNow.AddDays(-1) }
        );

        modelBuilder.Entity<OrderProduct>().HasData(
            new OrderProduct { OrderId = 1, ProductId = 1, Quantity = 2 },
            new OrderProduct { OrderId = 1, ProductId = 2, Quantity = 1 },
            new OrderProduct { OrderId = 2, ProductId = 3, Quantity = 3 }
        );
    }
}
