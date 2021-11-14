using MongoDB.Driver;

namespace Joker.Mongo.Context;

public abstract class MongoContext : IMongoContext
{
    protected MongoContext(IMongoDatabase database)
    {
        Database = database;
    }

    public IMongoDatabase Database { get; }
}