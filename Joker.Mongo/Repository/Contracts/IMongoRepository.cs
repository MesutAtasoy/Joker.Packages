using Joker.Repositories;

namespace Joker.Mongo.Repository.Contracts
{
    public interface IMongoRepository<T> : IRepository<T> where T : class
    {
    }
}
