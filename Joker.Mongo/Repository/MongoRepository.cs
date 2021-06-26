using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Joker.Mongo.Context;
using Joker.Mongo.Repository.Contracts;

namespace Joker.Mongo.Repository
{
    public class MongoRepository<T> : IMongoRepository<T> 
        where T : class
    {
        private readonly IMongoQueryRepository<T> _mongoQueryRepository;
        private readonly IMongoCommandRepository<T> _mongoCommandRepository;
        
        public MongoRepository(IMongoContext context)
        {
            _mongoQueryRepository = new MongoQueryRepository<T>(context);
            _mongoCommandRepository = new MongoCommandRepository<T>(context);
        }

        public virtual IQueryable<T> Get()
            => _mongoQueryRepository.Get();

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.Get(condition);
        
        public virtual Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.GetAsync(condition);

        public virtual T GetByKey(object key)
            => _mongoQueryRepository.GetByKey(key);

        public virtual Task<T> GetByKeyAsync(object key)
            => _mongoQueryRepository.GetByKeyAsync(key);

        public virtual bool Any()
            => _mongoQueryRepository.Any();

        public virtual bool Any(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.Any(condition);

        public virtual Task<bool> AnyAsync()
            => _mongoQueryRepository.AnyAsync();

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.AnyAsync(condition);

        public virtual long Count()
            => _mongoQueryRepository.Count();

        public virtual long Count(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.Count(condition);

        public virtual Task<long> CountAsync()
            => _mongoQueryRepository.CountAsync();

        public virtual Task<long> CountAsync(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.CountAsync(condition);

        public virtual T First()
            => _mongoQueryRepository.First();

        public virtual T First(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.First(condition);

        public virtual Task<T> FirstAsync()
            => _mongoQueryRepository.FirstAsync();

        public virtual Task<T> FirstAsync(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.FirstAsync(condition);

        public virtual T FirstOrDefault()
            => _mongoQueryRepository.FirstOrDefault();

        public virtual T FirstOrDefault(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.FirstOrDefault(condition);

        public virtual Task<T> FirstOrDefaultAsync()
            => _mongoQueryRepository.FirstOrDefaultAsync();

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.FirstOrDefaultAsync(condition);

        public virtual T Single()
            => _mongoQueryRepository.Single();

        public virtual T Single(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.Single();

        public virtual Task<T> SingleAsync()
            => _mongoQueryRepository.SingleAsync();

        public virtual Task<T> SingleAsync(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.SingleAsync(condition);

        public virtual T SingleOrDefault()
            => _mongoQueryRepository.SingleOrDefault();

        public virtual T SingleOrDefault(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.SingleOrDefault(condition);

        public virtual Task<T> SingleOrDefaultAsync()
            => _mongoQueryRepository.SingleOrDefaultAsync();

        public virtual Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> condition)
            => _mongoQueryRepository.SingleOrDefaultAsync(condition);

        public virtual T Add(T item)
            => _mongoCommandRepository.Add(item);

        public virtual Task<T> AddAsync(T item)
            => _mongoCommandRepository.AddAsync(item);

        public virtual void AddRange(IEnumerable<T> items)
            => _mongoCommandRepository.AddRange(items);

        public virtual Task AddRangeAsync(IEnumerable<T> items)
            => _mongoCommandRepository.AddRangeAsync(items);

        public virtual T Update(object key, T item)
            => _mongoCommandRepository.Update(key, item);

        public virtual Task<T> UpdateAsync(object key, T item)
            => _mongoCommandRepository.UpdateAsync(key, item);

        public virtual void Delete(object key)
            => _mongoCommandRepository.Delete(key);

        public virtual void Delete(Expression<Func<T, bool>> condition)
            => _mongoCommandRepository.Delete(condition);
        
        public virtual Task DeleteAsync(object key)
            => _mongoCommandRepository.DeleteAsync(key);
        
        public virtual Task DeleteAsync(Expression<Func<T, bool>> condition)
            => _mongoCommandRepository.DeleteAsync(condition);
    }
}
