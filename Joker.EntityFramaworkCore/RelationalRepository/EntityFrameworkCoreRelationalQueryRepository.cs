using Joker.Mapping;
using Joker.Objects.PagedList;
using Joker.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Joker.EntityFrameworkCore
{
    public class EntityFrameworkCoreRelationalQueryRepository<T> : EntityFrameworkCoreQueryRepository<T>,IRelationalQueryRepository<T>
        where T : class
    {
        public EntityFrameworkCoreRelationalQueryRepository(DbContext context) : base(context)
        {
            Queryable = context.Queryable<T>();
        }

        public IQueryable<T> Queryable { get; }

        public T FirstOrDefaultInclude(params Expression<Func<T, object>>[] include)
        {
            return Queryable.Include(include).FirstOrDefault();
        }

        public Task<T> FirstOrDefaultIncludeAsync(params Expression<Func<T, object>>[] include)
        {
            return Queryable.Include(include).FirstOrDefaultAsync();
        }

        public T FirstOrDefaultWhereInclude(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include)
        {
            return Queryable.Where(where).Include(include).FirstOrDefault();
        }

        public Task<T> FirstOrDefaultWhereIncludeAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include)
        {
            return Queryable.Where(where).Include(include).FirstOrDefaultAsync();
        }

        public TResult FirstOrDefaultWhereSelect<TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select)
        {
            return Queryable.Where(where).Select(select).FirstOrDefault();
        }

        public Task<TResult> FirstOrDefaultWhereSelectAsync<TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select)
        {
            return Queryable.Where(where).Select(select).FirstOrDefaultAsync();
        }

        public IEnumerable<T> ListInclude(params Expression<Func<T, object>>[] include)
        {
            return Queryable.Include(include).ToList();
        }

        public IEnumerable<TResult> ListInclude<TResult>(params Expression<Func<T, object>>[] include)
        {
            return Queryable.Include(include).Project<T, TResult>().ToList();
        }
        
        public async Task<IEnumerable<T>> ListIncludeAsync(params Expression<Func<T, object>>[] include)
        {
            return await Queryable.Include(include).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TResult>> ListIncludeAsync<TResult>(params Expression<Func<T, object>>[] include)
        {
            return await Queryable.Include(include).Project<T, TResult>().ToListAsync().ConfigureAwait(false);
        }

        public IEnumerable<T> ListWhereInclude(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include)
        {
            return Queryable.Where(where).Include(include).ToList();
        }

        public IEnumerable<TResult> ListWhereInclude<TResult>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include)
        {
            return Queryable.Where(where).Include(include).Project<T, TResult>().ToList();
        }

        public async Task<IEnumerable<T>> ListWhereIncludeAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include)
        {
            return await Queryable.Where(where).Include(include).ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<TResult>> ListWhereIncludeAsync<TResult>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include)
        {
            return await Queryable.Where(where).Include(include).Project<T, TResult>().ToListAsync().ConfigureAwait(false);
        }

        public T SingleOrDefaultWhereInclude(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include)
        {
            return Queryable.Where(where).Include(include).SingleOrDefault();
        }

        public Task<T> SingleOrDefaultWhereIncludeAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] include)
        {
            return Queryable.Where(where).Include(include).SingleOrDefaultAsync();
        }

        public TResult SingleOrDefaultWhereSelect<TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select)
        {
            return Queryable.Where(where).Select(select).SingleOrDefault();
        }

        public Task<TResult> SingleOrDefaultWhereSelectAsync<TResult>(Expression<Func<T, bool>> where, Expression<Func<T, TResult>> select)
        {
            return Queryable.Where(where).Select(select).SingleOrDefaultAsync();
        }
    }
}
