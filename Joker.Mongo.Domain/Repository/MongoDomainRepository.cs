using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Joker.Mongo.Domain.Context;
using Joker.Mongo.Domain.Repository.Contracts;

namespace Joker.Mongo.Domain.Repository
{
    public class MongoDomainDomainRepository<T> : IMongoDomainRepository<T> 
        where T : class
    {
        private readonly IMongoDomainQueryRepository<T> _mongoDomainQueryRepository;
        private readonly IMongoDomainCommandRepository<T> _mongoDomainCommandRepository;
        
        public MongoDomainDomainRepository(IMongoDomainContext domainContext)
        {
            _mongoDomainQueryRepository = new MongoDomainQueryRepository<T>(domainContext);
            _mongoDomainCommandRepository = new MongoDomainCommandRepository<T>(domainContext);
        }

        public virtual IQueryable<T> Get()
            => _mongoDomainQueryRepository.Get();

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.Get(condition);
        
        public virtual Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.GetAsync(condition);

        public virtual T GetByKey(object key)
            => _mongoDomainQueryRepository.GetByKey(key);

        public virtual Task<T> GetByKeyAsync(object key)
            => _mongoDomainQueryRepository.GetByKeyAsync(key);

        public virtual bool Any()
            => _mongoDomainQueryRepository.Any();

        public virtual bool Any(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.Any(condition);

        public virtual Task<bool> AnyAsync()
            => _mongoDomainQueryRepository.AnyAsync();

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.AnyAsync(condition);

        public virtual long Count()
            => _mongoDomainQueryRepository.Count();

        public virtual long Count(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.Count(condition);

        public virtual Task<long> CountAsync()
            => _mongoDomainQueryRepository.CountAsync();

        public virtual Task<long> CountAsync(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.CountAsync(condition);

        public virtual T First()
            => _mongoDomainQueryRepository.First();

        public virtual T First(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.First(condition);

        public virtual Task<T> FirstAsync()
            => _mongoDomainQueryRepository.FirstAsync();

        public virtual Task<T> FirstAsync(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.FirstAsync(condition);

        public virtual T FirstOrDefault()
            => _mongoDomainQueryRepository.FirstOrDefault();

        public virtual T FirstOrDefault(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.FirstOrDefault(condition);

        public virtual Task<T> FirstOrDefaultAsync()
            => _mongoDomainQueryRepository.FirstOrDefaultAsync();

        public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.FirstOrDefaultAsync(condition);

        public virtual T Single()
            => _mongoDomainQueryRepository.Single();

        public virtual T Single(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.Single();

        public virtual Task<T> SingleAsync()
            => _mongoDomainQueryRepository.SingleAsync();

        public virtual Task<T> SingleAsync(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.SingleAsync(condition);

        public virtual T SingleOrDefault()
            => _mongoDomainQueryRepository.SingleOrDefault();

        public virtual T SingleOrDefault(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.SingleOrDefault(condition);

        public virtual Task<T> SingleOrDefaultAsync()
            => _mongoDomainQueryRepository.SingleOrDefaultAsync();

        public virtual Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> condition)
            => _mongoDomainQueryRepository.SingleOrDefaultAsync(condition);

        public virtual T Add(T item)
            => _mongoDomainCommandRepository.Add(item);

        public virtual Task<T> AddAsync(T item)
            => _mongoDomainCommandRepository.AddAsync(item);

        public virtual void AddRange(IEnumerable<T> items)
            => _mongoDomainCommandRepository.AddRange(items);

        public virtual Task AddRangeAsync(IEnumerable<T> items)
            => _mongoDomainCommandRepository.AddRangeAsync(items);

        public virtual T Update(object key, T item)
            => _mongoDomainCommandRepository.Update(key, item);

        public virtual Task<T> UpdateAsync(object key, T item)
            => _mongoDomainCommandRepository.UpdateAsync(key, item);

        public virtual void Delete(object key)
            => _mongoDomainCommandRepository.Delete(key);

        public virtual void Delete(Expression<Func<T, bool>> condition)
            => _mongoDomainCommandRepository.Delete(condition);
        
        public virtual Task DeleteAsync(object key)
            => _mongoDomainCommandRepository.DeleteAsync(key);
        
        public virtual Task DeleteAsync(Expression<Func<T, bool>> condition)
            => _mongoDomainCommandRepository.DeleteAsync(condition);
    }
}
