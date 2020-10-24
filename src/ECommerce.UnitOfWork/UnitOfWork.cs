using System;
using ECommerce.Data;
using ECommerce.Entity;
using Ecommerce.Repository;

namespace ECommerce.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceContext _context;
        private bool disposed;
        private IRepository<Product> _productRepository;
        private IRepository<Order> _orderRepository;
        public IRepository<Product> ProductRepository => _productRepository ??= new Repository<Product>(_context);
        public IRepository<Order> OrderRepository => _orderRepository ??= new Repository<Order>(_context);

        public UnitOfWork(ECommerceContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}