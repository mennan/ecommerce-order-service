using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ECommerce.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ECommerceContext _context;
        private readonly DbSet<TEntity> _entity;

        public Repository(ECommerceContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }

        public List<TEntity> FindAll()
        {
            return _entity.ToList();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.Where(predicate).AsQueryable();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entity.FirstOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            _entity.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _entity.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }
    }
}