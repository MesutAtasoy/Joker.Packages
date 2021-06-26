using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Joker.Exceptions;
using Joker.Mongo.Context;
using Joker.Mongo.Repository.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Joker.Mongo.Repository
{
    public class MongoCommandRepository<T> : IMongoCommandRepository<T> where T : class
    {
        private IMongoCollection<T> Collection { get; }

        public MongoCommandRepository(IMongoContext context)
        {
            Check.NotNull(context, nameof(context));

            Collection = context.Database.GetCollection<T>(typeof(T).Name);
        }

        public virtual T Add(T item)
        {
            Collection.InsertOne(item);
            
            return item;
        }

        public virtual async Task<T> AddAsync(T item)
        { 
            await Collection.InsertOneAsync(item);
            
            return await Task.FromResult(item);
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            Collection.InsertMany(items);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> items)
        {
            await Collection.InsertManyAsync(items);
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
    }
}