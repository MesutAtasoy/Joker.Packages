using MongoDB.Driver;

namespace Joker.Mongo.Context
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }
}