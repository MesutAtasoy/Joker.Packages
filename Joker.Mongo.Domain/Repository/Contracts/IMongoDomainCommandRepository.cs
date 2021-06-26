using Joker.Repositories;

namespace Joker.Mongo.Domain.Repository.Contracts
{
    public interface IMongoDomainCommandRepository<T> : ICommandRepository<T> where T : class
    {
    }
}
