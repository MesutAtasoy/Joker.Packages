using System.Linq.Expressions;

namespace Joker.Repositories;

public interface ICommandRepository<T> where T : class
{
    T Add(T item);
    Task<T> AddAsync(T item);
    void AddRange(IEnumerable<T> items);
    Task AddRangeAsync(IEnumerable<T> items);
    T Update(object key, T item);
    Task<T> UpdateAsync(object key, T item);
    void Delete(object key);
    void Delete(Expression<Func<T, bool>> condition);
    Task DeleteAsync(object key);
    Task DeleteAsync(Expression<Func<T, bool>> condition);
}