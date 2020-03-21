namespace Joker.Repositories
{
    public interface IRelationalCommandRepository<T> : ICommandRepository<T> where T : class { }
}
