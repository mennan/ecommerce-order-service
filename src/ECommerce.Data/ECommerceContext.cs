using System;
using System.Collections.Generic;
using ECommerce.Entity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Data
{
    public class ECommerceContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.Parse("35984747-3FB3-4847-A573-91A21F23EB82"),
                    Code = "iphone",
                    Name = "iPhone 11",
                    Price = 10000,
                    Stock = 20
                },
                new Product
                {
                    Id = Guid.Parse("446A2038-90D1-4A52-A680-E54D2B6DCEC8"),
                    Code = "mbp",
                    Name = "Macbook Pro 16",
                    Price = 22750,
                    Stock = 40
                }
            };
            
            modelBuilder.Entity<Product>().HasData(products);
            base.OnModelCreating(modelBuilder);
        }
    }
}