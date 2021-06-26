using MongoDB.Driver;

namespace Joker.Mongo.Domain.Context
{
    public abstract class MongoDomainContext : IMongoDomainContext
    {
        protected MongoDomainContext(IMongoDatabase database)
        {
            Database = database;
        }

        public IMongoDatabase Database { get; }
    }
}
