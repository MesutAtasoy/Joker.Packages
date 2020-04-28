using Joker.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Joker.EntityFrameworkCore
{
    public class EntityFrameworkCoreRepository<T> : IRepository<T> where T : class
    {
        public EntityFrameworkCoreRepository(DbContext context)
        {
            EntityFrameworkCoreCommandRepository = new EntityFrameworkCoreCommandRepository<T>(context);
            EntityFrameworkCoreQueryRepository = new EntityFrameworkCoreQueryRepository<T>(context);
        }

        private EntityFrameworkCoreCommandRepository<T> EntityFrameworkCoreCommandRepository { get; }

        private EntityFrameworkCoreQueryRepository<T> EntityFrameworkCoreQueryRepository { get; }

        public void Add(T item)
        {
            EntityFrameworkCoreCommandRepository.Add(item);
        }

        public Task AddAsync(T item)
        {
            return EntityFrameworkCoreCommandRepository.AddAsync(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            EntityFrameworkCoreCommandRepository.AddRange(items);
        }

        public Task AddRangeAsync(IEnumerable<T> items)
        {
            return EntityFrameworkCoreCommandRepository.AddRangeAsync(items);
        }

        public bool Any()
        {
            return EntityFrameworkCoreQueryRepository.Any();
        }

        public bool Any(Expression<Func<T, bool>> where)
        {
            return EntityFrameworkCoreQueryRepository.Any(where);
        }

        public Task<bool> AnyAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.AnyAsync(cancellationToken);
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.AnyAsync(where, cancellationToken);
        }

        public long Count()
        {
            return EntityFrameworkCoreQueryRepository.Count();
        }

        public long Count(Expression<Func<T, bool>> where)
        {
            return EntityFrameworkCoreQueryRepository.Count(where);
        }

        public Task<long> CountAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.CountAsync(cancellationToken);
        }

        public Task<long> CountAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.CountAsync(where, cancellationToken);
        }

        public void Delete(object key)
        {
            EntityFrameworkCoreCommandRepository.Delete(key);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            EntityFrameworkCoreCommandRepository.Delete(where);
        }

        public Task DeleteAsync(object key)
        {
            return EntityFrameworkCoreCommandRepository.DeleteAsync(key);
        }

        public Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            return EntityFrameworkCoreCommandRepository.DeleteAsync(where);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            return EntityFrameworkCoreQueryRepository.FirstOrDefault(where);
        }

        public TResult FirstOrDefault<TResult>(Expression<Func<T, bool>> where)
        {
            return EntityFrameworkCoreQueryRepository.FirstOrDefault<TResult>(where);
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.FirstOrDefaultAsync(where, cancellationToken);
        }

        public Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T, bool>> where,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.FirstOrDefaultAsync<TResult>(where, cancellationToken);
        }

        public IQueryable<T> GetAsQueryable()
        {
            return EntityFrameworkCoreQueryRepository.GetAsQueryable();
        }

        public IEnumerable<T> List()
        {
            return EntityFrameworkCoreQueryRepository.List();
        }

        public IEnumerable<TResult> List<TResult>()
        {
            return EntityFrameworkCoreQueryRepository.List<TResult>();
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> where)
        {
            return EntityFrameworkCoreQueryRepository.List(where);
        }

        public IEnumerable<TResult> List<TResult>(Expression<Func<T, bool>> where)
        {
            return EntityFrameworkCoreQueryRepository.List<TResult>(where);
        }

        public Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.ListAsync(cancellationToken);
        }

        public Task<IEnumerable<TResult>> ListAsync<TResult>(CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.ListAsync<TResult>(cancellationToken);
        }

        public Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.ListAsync(where, cancellationToken);
        }

        public Task<IEnumerable<TResult>> ListAsync<TResult>(Expression<Func<T, bool>> where,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.ListAsync<TResult>(where, cancellationToken);
        }

        public T Select(object key)
        {
            return EntityFrameworkCoreQueryRepository.Select(key);
        }

        public TResult Select<TResult>(object key)
        {
            return EntityFrameworkCoreQueryRepository.Select<TResult>(key);
        }

        public Task<T> SelectAsync(object key, CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.SelectAsync(key, cancellationToken);
        }

        public Task<TResult> SelectAsync<TResult>(object key, CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.SelectAsync<TResult>(key, cancellationToken);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> where)
        {
            return EntityFrameworkCoreQueryRepository.SingleOrDefault(where);
        }

        public TResult SingleOrDefault<TResult>(Expression<Func<T, bool>> where)
        {
            return EntityFrameworkCoreQueryRepository.SingleOrDefault<TResult>(where);
        }

        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.SingleOrDefaultAsync(where,cancellationToken);
        }

        public Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default(CancellationToken))
        {
            return EntityFrameworkCoreQueryRepository.SingleOrDefaultAsync<TResult>(where,cancellationToken);
        }

        public void Update(object key, T item)
        {
            EntityFrameworkCoreCommandRepository.Update(key, item);
        }

        public Task UpdateAsync(object key, T item)
        {
            return EntityFrameworkCoreCommandRepository.UpdateAsync(key, item);
        }

        public void UpdatePartial(object key, object item)
        {
            EntityFrameworkCoreCommandRepository.UpdatePartial(key, item);
        }

        public Task UpdatePartialAsync(object key, object item)
        {
            return EntityFrameworkCoreCommandRepository.UpdatePartialAsync(key, item);
        }
    }
}