using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderProcessingSystemAPI.Models;

namespace OrderProcessingSystemAPI.Data
{
    public class OrderProcessingSystemAPIContext : DbContext
    {
        public OrderProcessingSystemAPIContext (DbContextOptions<OrderProcessingSystemAPIContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .HasMany(o => o.OrderItems);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders);

            modelBuilder.Entity<Customer>().HasData(
            new Customer { CustomerId = 1, Name = "Alice" },
            new Customer { CustomerId = 2, Name = "Bob" }
        );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Laptop", Price = 1000 },
                new Product { ProductId = 2, Name = "Smartphone", Price = 500 },
                new Product { ProductId = 3, Name = "Headphones", Price = 100 }
            );

            // Seed Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, CustomerId = 1, IsFulfilled = false },
                new Order { OrderId = 2, CustomerId = 2, IsFulfilled = true }
            );

            // Seed OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { OrderItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1 },
                new OrderItem { OrderItemId = 2, OrderId = 1, ProductId = 2, Quantity = 2 },
                new OrderItem { OrderItemId = 3, OrderId = 2, ProductId = 3, Quantity = 1 }
            );

        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
    }
}
