using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Joker.Domain.Entities;
using Joker.EventBus;
using Joker.Exceptions;
using Joker.Mongo.Domain.Context;
using Joker.Mongo.Domain.Repository.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Joker.Mongo.Domain.Repository
{
    public class MongoDomainCommandRepository<T> : IMongoDomainCommandRepository<T> where T : class
    {
        private IMongoCollection<T> Collection { get; }
        private readonly IEventDispatcher _domainEventDispatcher;

        public MongoDomainCommandRepository(IMongoDomainContext context, IEventDispatcher domainEventDispatcher)
        {
            Check.NotNull(context, nameof(context));

            _domainEventDispatcher = domainEventDispatcher;
            Collection = context.Database.GetCollection<T>(typeof(T).Name);
        }

        public virtual T Add(T item)
        {
            Collection.InsertOne(item);
            DispatchDomainEvents(item).Wait();

            return item;
        }

        public virtual async Task<T> AddAsync(T item)
        {
            await Collection.InsertOneAsync(item);
            await DispatchDomainEvents(item);

            return await Task.FromResult(item);
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            Collection.InsertMany(items);
            DispatchDomainEvents(items).Wait();
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> items)
        {
            await Collection.InsertManyAsync(items);
            await DispatchDomainEvents(items);
        }

        public virtual T Update(object key, T item)
        {
            var result = Collection.FindOneAndReplace(
                FilterId(key),
                item,
                new FindOneAndReplaceOptions<T>
                {
                    IsUpsert = false,
                    ReturnDocument = ReturnDocument.After
                });

            DispatchDomainEvents(item).Wait();
            return result;
        }

        public virtual async Task<T> UpdateAsync(object key, T item)
        {
            var result = await Collection.FindOneAndReplaceAsync(
                FilterId(key),
                item,
                new FindOneAndReplaceOptions<T>
                {
                    IsUpsert = false,
                    ReturnDocument = ReturnDocument.After
                });

            DispatchDomainEvents(item).Wait();
            return result;
        }

        public virtual void Delete(object key)
        {
            Collection.DeleteOne(FilterId(key));
        }

        public virtual async Task DeleteAsync(object key)
        {
            await Collection.DeleteOneAsync(FilterId(key));
        }

        public virtual void Delete(Expression<Func<T, bool>> condition)
        {
            Collection.DeleteOne(condition);
        }

        public virtual async Task DeleteAsync(Expression<Func<T, bool>> condition)
        {
            await Collection.DeleteOneAsync(condition);
        }

        protected FilterDefinition<T> FilterId(object key)
        {
            if (key is Guid guidKey)
            {
                return Builders<T>.Filter.Eq(new StringFieldDefinition<T, Guid>("_id"), guidKey);
            }

            return Builders<T>.Filter.Eq("_id", ObjectId.Parse(key.ToString()));
        }

        #region Private Helpers Of DomainEvent

        private async Task DispatchDomainEvents(T entity)
        {
            if (_domainEventDispatcher == null || entity == null)
                return;

            if (entity is DomainEntity domainEntity
                && domainEntity.DomainEvents != null
                && domainEntity.DomainEvents.Any())
            {
                await _domainEventDispatcher.Dispatch(domainEntity.DomainEvents);
                domainEntity.ClearDomainEvents();
            }
        }

        private async Task DispatchDomainEvents(IEnumerable<T> entities)
        {
            if (_domainEventDispatcher == null || entities == null)
                return;

            IEnumerable<DomainEntity> domainEntities = entities
                .Where(e => e is DomainEntity)
                .Cast<DomainEntity>();

            foreach (DomainEntity domainEntity in domainEntities)
            {
                if (domainEntity.DomainEvents != null && domainEntity.DomainEvents.Any())
                {
                    await _domainEventDispatcher.Dispatch(domainEntity.DomainEvents);
                    domainEntity.ClearDomainEvents();
                }
            }
        }

        #endregion
    }
}