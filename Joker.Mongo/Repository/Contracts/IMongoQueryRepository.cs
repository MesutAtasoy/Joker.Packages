using Joker.Repositories;

namespace Joker.Mongo.Repository.Contracts;

public interface IMongoQueryRepository<T> : IQueryRepository<T> where T : class
{
}