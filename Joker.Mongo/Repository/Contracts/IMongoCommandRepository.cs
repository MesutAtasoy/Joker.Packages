using Joker.Repositories;

namespace Joker.Mongo.Repository.Contracts
{
    public interface IMongoCommandRepository<T> : ICommandRepository<T> where T : class
    {
    }
}
