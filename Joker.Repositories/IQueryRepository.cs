using System.Linq.Expressions;

namespace Joker.Repositories;

public interface IQueryRepository<T> where T : class
{
    IQueryable<T> Get();
    IQueryable<T> Get(Expression<Func<T, bool>> condition);
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> condition);

    T GetByKey(object key);
    Task<T> GetByKeyAsync(object key);

    bool Any();
    bool Any(Expression<Func<T, bool>> condition);
    Task<bool> AnyAsync();
    Task<bool> AnyAsync(Expression<Func<T, bool>> condition);

    long Count();
    long Count(Expression<Func<T, bool>> condition);
    Task<long> CountAsync();
    Task<long> CountAsync(Expression<Func<T, bool>> condition);

    T First();
    T First(Expression<Func<T, bool>> condition);
    Task<T> FirstAsync();
    Task<T> FirstAsync(Expression<Func<T, bool>> condition);

    T FirstOrDefault();
    T FirstOrDefault(Expression<Func<T, bool>> condition);
    Task<T> FirstOrDefaultAsync();
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> condition);

    T Single();
    T Single(Expression<Func<T, bool>> condition);
    Task<T> SingleAsync();
    Task<T> SingleAsync(Expression<Func<T, bool>> condition);

    T SingleOrDefault();
    T SingleOrDefault(Expression<Func<T, bool>> condition);
    Task<T> SingleOrDefaultAsync();
    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> condition);
}