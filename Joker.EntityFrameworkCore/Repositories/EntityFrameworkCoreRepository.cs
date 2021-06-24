using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Joker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Joker.EntityFrameworkCore.Repositories
{
    public class EntityFrameworkCoreRepository<TContext, TEntity> : EntityFrameworkCoreQueryRepository<TContext, TEntity>,
            IRepository<TEntity>
        where TContext : DbContext
        where TEntity : class
    {
        public EntityFrameworkCoreRepository(TContext dbContext) : base(dbContext)
        {
        }

        public TEntity Add(TEntity item)
            => DbContext.Add(item).Entity;

        public async Task<TEntity> AddAsync(TEntity item)
            => (await DbContext.AddAsync(item)).Entity;

        public void AddRange(IEnumerable<TEntity> items)
            => DbContext.AddRange(items);

        public async Task AddRangeAsync(IEnumerable<TEntity> items)
            => await DbContext.AddRangeAsync(items);

        public TEntity Update(object key, TEntity item)
        {
            var entity = DbSet.Find(key);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbContext.Entry(entity).CurrentValues.SetValues(item);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(object key, TEntity item)
        {
            var entity = await DbSet.FindAsync(key);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbContext.Entry(entity).CurrentValues.SetValues(item);
            return entity;
        }

        public void Delete(object key)
        {
            var entity = DbSet.Find(key);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbContext.Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> condition)
        {
            List<TEntity> entities = DbSet.Where(condition).ToList();

            foreach (TEntity entity in entities)
            {
                DbContext.Remove(entity);
            }
        }

        public async Task DeleteAsync(object key)
        {
            var entity = await DbSet.FindAsync(key);
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            DbContext.Remove(entity);
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> condition)
        {
            List<TEntity> entities = await DbSet.Where(condition).ToListAsync();

            foreach (TEntity entity in entities)
            {
                DbContext.Remove(entity);
            }
        }
    }
}