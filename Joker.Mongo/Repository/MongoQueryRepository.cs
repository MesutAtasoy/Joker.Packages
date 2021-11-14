using System.Linq.Expressions;
using Joker.Mongo.Context;
using Joker.Mongo.Repository.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Joker.Mongo.Repository;

public class MongoQueryRepository<T> : IMongoQueryRepository<T> where T : class
{
    private IMongoCollection<T> Collection { get; }

    public MongoQueryRepository(IMongoContext context)
    {
        if (context == default)
            throw new ArgumentNullException(nameof(context));

        Collection = context.Database.GetCollection<T>(typeof(T).Name);
    }

    public virtual IQueryable<T> Get()
        => Collection.AsQueryable();

    public virtual IQueryable<T> Get(Expression<Func<T, bool>> condition)
        => Get().Where(condition);
        
    public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> condition)
        => await Collection.Find(condition).ToListAsync().ConfigureAwait(false);

    public virtual T GetByKey(object key)
        => Collection.Find(FilterId(key)).SingleOrDefault();

    public virtual Task<T> GetByKeyAsync(object key)
        => Collection.Find(FilterId(key)).SingleOrDefaultAsync();

    public virtual bool Any()
        => Collection.Find(FilterDefinition<T>.Empty).Any();

    public virtual bool Any(Expression<Func<T, bool>> condition)
        => Collection.Find(condition).Any();

    public virtual Task<bool> AnyAsync()
        => Collection.Find(FilterDefinition<T>.Empty).AnyAsync();

    public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> condition)
        => Collection.Find(condition).AnyAsync();

    public virtual long Count()
        => Collection.CountDocuments(FilterDefinition<T>.Empty);

    public virtual long Count(Expression<Func<T, bool>> condition)
        => Collection.CountDocuments(condition);

    public virtual Task<long> CountAsync()
        => Collection.CountDocumentsAsync(FilterDefinition<T>.Empty);

    public virtual Task<long> CountAsync(Expression<Func<T, bool>> condition)
        => Collection.CountDocumentsAsync(condition);

    public virtual T First()
        => Collection.Find(FilterDefinition<T>.Empty).First();

    public virtual T First(Expression<Func<T, bool>> condition)
        => Collection.Find(condition).First();

    public virtual Task<T> FirstAsync()
        => Collection.Find(FilterDefinition<T>.Empty).FirstAsync();

    public virtual Task<T> FirstAsync(Expression<Func<T, bool>> condition)
        => Collection.Find(condition).FirstAsync();

    public virtual T FirstOrDefault()
        => Collection.Find(FilterDefinition<T>.Empty).FirstOrDefault();

    public virtual T FirstOrDefault(Expression<Func<T, bool>> condition)
        => Collection.Find(condition).FirstOrDefault();

    public virtual Task<T> FirstOrDefaultAsync()
        => Collection.Find(FilterDefinition<T>.Empty).FirstOrDefaultAsync();

    public virtual Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> condition)
        => Collection.Find(condition).FirstOrDefaultAsync();

    public virtual T Single()
        => Collection.Find(FilterDefinition<T>.Empty).Single();

    public virtual T Single(Expression<Func<T, bool>> condition)
        => Collection.Find(condition).Single();

    public virtual Task<T> SingleAsync()
        => Collection.Find(FilterDefinition<T>.Empty).SingleAsync();

    public virtual Task<T> SingleAsync(Expression<Func<T, bool>> condition)
        => Collection.Find(condition).SingleAsync();

    public virtual T SingleOrDefault()
        => Collection.Find(FilterDefinition<T>.Empty).SingleOrDefault();

    public virtual T SingleOrDefault(Expression<Func<T, bool>> condition)
        => Collection.Find(condition).SingleOrDefault();

    public virtual Task<T> SingleOrDefaultAsync()
        => Collection.Find(FilterDefinition<T>.Empty).SingleOrDefaultAsync();

    public virtual Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> condition)
        => Collection.Find(condition).SingleOrDefaultAsync();

    protected FilterDefinition<T> FilterId(object key)
    {
        if (key is Guid guidKey)
        {
            return Builders<T>.Filter.Eq(new StringFieldDefinition<T, Guid>("_id"), guidKey);
        }
            
        return Builders<T>.Filter.Eq("_id", ObjectId.Parse(key.ToString()));
    }
}