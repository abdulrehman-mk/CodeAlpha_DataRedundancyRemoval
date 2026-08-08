using CodeAlpha_DataRedundancyRemoval.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeAlpha_DataRedundancyRemoval.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---- Relationships (normalized, no redundant data) ----

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // ---- Seed data (static values -> fixed keys required by HasData) ----

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Phones, laptops, and accessories" },
                new Category { Id = 2, Name = "Books", Description = "Fiction and non-fiction books" },
                new Category { Id = 3, Name = "Groceries", Description = "Everyday food and household items" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Wireless Mouse", Price = 15.99m, Stock = 120, CategoryId = 1 },
                new Product { Id = 2, Name = "Mechanical Keyboard", Price = 49.99m, Stock = 60, CategoryId = 1 },
                new Product { Id = 3, Name = "27-inch Monitor", Price = 189.00m, Stock = 25, CategoryId = 1 },
                new Product { Id = 4, Name = "Clean Code", Price = 32.50m, Stock = 40, CategoryId = 2 },
                new Product { Id = 5, Name = "The Pragmatic Programmer", Price = 29.75m, Stock = 35, CategoryId = 2 },
                new Product { Id = 6, Name = "Organic Coffee Beans (1kg)", Price = 12.25m, Stock = 200, CategoryId = 3 }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Ayesha Khan", Email = "ayesha.khan@example.com", Phone = "+92-300-1112222", Address = "Clifton, Karachi" },
                new Customer { Id = 2, Name = "Bilal Ahmed", Email = "bilal.ahmed@example.com", Phone = "+92-301-2223333", Address = "Gulshan-e-Iqbal, Karachi" },
                new Customer { Id = 3, Name = "Sara Malik", Email = "sara.malik@example.com", Phone = "+92-302-3334444", Address = "DHA Phase 5, Karachi" }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, OrderDate = new DateTime(2026, 6, 10), CustomerId = 1 },
                new Order { Id = 2, OrderDate = new DateTime(2026, 6, 15), CustomerId = 2 },
                new Order { Id = 3, OrderDate = new DateTime(2026, 7, 1), CustomerId = 1 }
            );

            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 15.99m },
                new OrderDetail { Id = 2, OrderId = 1, ProductId = 4, Quantity = 1, UnitPrice = 32.50m },
                new OrderDetail { Id = 3, OrderId = 2, ProductId = 3, Quantity = 1, UnitPrice = 189.00m },
                new OrderDetail { Id = 4, OrderId = 3, ProductId = 6, Quantity = 3, UnitPrice = 12.25m },
                new OrderDetail { Id = 5, OrderId = 3, ProductId = 2, Quantity = 1, UnitPrice = 49.99m }
            );
        }
    }
}
