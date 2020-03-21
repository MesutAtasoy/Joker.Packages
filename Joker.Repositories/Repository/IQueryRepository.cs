using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Joker.Repositories
{
    public interface IQueryRepository<T> where T : class
    {
        IQueryable<T> GetAsQueryable();

        bool Any();

        bool Any(Expression<Func<T, bool>> where);

        Task<bool> AnyAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> AnyAsync(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken));

        long Count();

        long Count(Expression<Func<T, bool>> where);

        Task<long> CountAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<long> CountAsync(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken));

        T FirstOrDefault(Expression<Func<T, bool>> where);

        TResult FirstOrDefault<TResult>(Expression<Func<T, bool>> where);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken));

        Task<TResult> FirstOrDefaultAsync<TResult>(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken));

        IEnumerable<T> List();

        IEnumerable<T> List(Expression<Func<T, bool>> where);

        IEnumerable<TResult> List<TResult>();

        IEnumerable<TResult> List<TResult>(Expression<Func<T, bool>> where);
        
        Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<TResult>> ListAsync<TResult>(CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<TResult>> ListAsync<TResult>(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken));

        T Select(object key);

        TResult Select<TResult>(object key);

        Task<T> SelectAsync(object key,CancellationToken cancellationToken = default(CancellationToken));

        Task<TResult> SelectAsync<TResult>(object key,CancellationToken cancellationToken = default(CancellationToken));

        T SingleOrDefault(Expression<Func<T, bool>> where);

        TResult SingleOrDefault<TResult>(Expression<Func<T, bool>> where);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken));

        Task<TResult> SingleOrDefaultAsync<TResult>(Expression<Func<T, bool>> where,CancellationToken cancellationToken = default(CancellationToken));
    }
}
