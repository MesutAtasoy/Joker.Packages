using System;
using System.Threading;
using System.Threading.Tasks;

namespace Joker.Repositories
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}