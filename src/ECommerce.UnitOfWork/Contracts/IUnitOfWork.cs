using System;
using ECommerce.Entity;
using Ecommerce.Repository;

namespace ECommerce.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        IRepository<Product> ProductRepository { get; }
        IRepository<Order> OrderRepository { get; }
    }
}