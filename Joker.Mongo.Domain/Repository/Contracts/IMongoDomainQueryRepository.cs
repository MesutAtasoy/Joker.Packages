using Joker.Repositories;

namespace Joker.Mongo.Domain.Repository.Contracts
{
    public interface IMongoDomainQueryRepository<T> : IQueryRepository<T> where T : class
    {
    }
}
