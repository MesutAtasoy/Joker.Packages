using Joker.Repositories;

namespace Joker.Mongo.Domain.Repository.Contracts
{
    public interface IMongoDomainRepository<T> : IRepository<T> where T : class
    {
    }
}
