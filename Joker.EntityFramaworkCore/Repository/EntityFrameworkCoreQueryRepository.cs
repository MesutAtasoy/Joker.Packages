using Joker.Mapping;
using Joker.Objects.PagedList;
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
    public class EntityFrameworkCoreQueryRepository<T> : IQueryRepository<T> where T : class
    {
        public EntityFrameworkCoreQueryRepository(DbContext context)
        {
            Context = context;
        }

        private IQueryable<T> Queryable => Context.Queryable<T>();

        private DbContext Context { get; }

        public bool Any()
        {
            return Queryable.Any();
        }

        public bool Any(Expression<Func<T, bool>> where)
        {
            return Queryable.Any(where);
        }

        public Task<bool> AnyAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Queryable.AnyAsync(cancellationToken);
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken))
        {
            return Queryable.AnyAsync(where,cancellationToken);
        }

        public long Count()
        {
            return Queryable.LongCount();
        }

        public long Count(Expression<Func<T, bool>> where)
        {
            return Queryable.LongCount(where);
        }

        public Task<long> CountAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return Queryable.LongCountAsync(cancellationToken);
        }

        public Task<long> CountAsync(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken))
        {
            return Queryable.LongCountAsync(where,cancellationToken);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> where)
        {
            return Queryable.FirstOrDefault(where);
        }

        public TResult FirstOrDefault<TResult>(Expression<Func<T, bool>> where)
        {
            return Queryable.Where(where).Project<T, TResult>().FirstOrDefault();
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Queryable.FirstOrDefaultAsync(where,cancellationToken);
        }

        public Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken))
        {
            return Queryable.Where(where).Project<T, TResult>().FirstOrDefaultAsync(cancellationToken);
        }

        public IQueryable<T> GetAsQueryable()
        {
            return Queryable;
        }

        public IEnumerable<T> List()
        {
            return Queryable.ToList();
        }

        public IEnumerable<TResult> List<TResult>()
        {
            return Queryable.Project<T, TResult>().ToList();
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> where)
        {
            return Queryable.Where(where).ToList();
        }

        public IEnumerable<TResult> List<TResult>(Expression<Func<T, bool>> where)
        {
            return Queryable.Where(where).Project<T, TResult>().ToList();
        }

        public async Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Queryable.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TResult>> ListAsync<TResult>(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Queryable.Project<T, TResult>().ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Queryable.Where(where).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TResult>> ListAsync<TResult>(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Queryable.Where(where).Project<T, TResult>().ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public T Select(object key)
        {
            Context.DetectChangesLazyLoading(false);

            return Context.Set<T>().Find(key);
        }

        public TResult Select<TResult>(object key)
        {
            return Select(key).Map<T, TResult>();
        }

        public Task<T> SelectAsync(object key,CancellationToken cancellationToken = default(CancellationToken))
        {
            Context.DetectChangesLazyLoading(false);

            return Context.Set<T>().FindAsync(key,cancellationToken).AsTask();
        }

        public Task<TResult> SelectAsync<TResult>(object key,CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult(SelectAsync(key,cancellationToken).Result.Map<TResult>());
        }

        public T SingleOrDefault(Expression<Func<T, bool>> where)
        {
            return Queryable.SingleOrDefault(where);
        }

        public TResult SingleOrDefault<TResult>(Expression<Func<T, bool>> where)
        {
            return Queryable.Where(where).Project<T, TResult>().SingleOrDefault();
        }

        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken))
        {
            return Queryable.SingleOrDefaultAsync(where,cancellationToken);
        }

        public Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken))
        {
            return Queryable.Where(where).Project<T, TResult>().SingleOrDefaultAsync(cancellationToken);
        }
    }
}
