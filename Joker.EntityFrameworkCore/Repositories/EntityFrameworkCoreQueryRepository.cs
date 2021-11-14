using System.Linq.Expressions;
using Joker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Joker.EntityFrameworkCore.Repositories;

/// <summary>
/// Warning: All methods call with AsNoTracking
/// </summary>
public class EntityFrameworkCoreQueryRepository<TContext, TEntity> : IQueryRepository<TEntity> 
    where TContext : DbContext
    where TEntity : class
{
    protected readonly TContext DbContext;
    protected readonly DbSet<TEntity> DbSet;

    public EntityFrameworkCoreQueryRepository(TContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public IQueryable<TEntity> Get()
        => DbSet;

    public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> condition)
        => DbSet.Where(condition);

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> condition)
        => await DbSet.Where(condition).ToListAsync();

    public TEntity GetByKey(object key)
        => DbSet.Find(key);

    public async Task<TEntity> GetByKeyAsync(object key)
        => await DbSet.FindAsync(key);

    public bool Any()
        => DbSet.Any();

    public bool Any(Expression<Func<TEntity, bool>> condition)
        => DbSet.Any(condition);

    public async Task<bool> AnyAsync()
        => await DbSet.AnyAsync();

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> condition)
        => await DbSet.AnyAsync(condition);

    public long Count()
        => DbSet.Count();

    public long Count(Expression<Func<TEntity, bool>> condition)
        => DbSet.Count(condition);

    public async Task<long> CountAsync()
        => await DbSet.CountAsync();

    public async Task<long> CountAsync(Expression<Func<TEntity, bool>> condition)
        => await DbSet.CountAsync(condition);

    public TEntity First()
        => DbSet.First();

    public TEntity First(Expression<Func<TEntity, bool>> condition)
        => DbSet.First(condition);

    public async Task<TEntity> FirstAsync()
        => await DbSet.FirstAsync();

    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> condition)
        => await DbSet.FirstAsync(condition);

    public TEntity FirstOrDefault()
        => DbSet.FirstOrDefault();

    public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> condition)
        => DbSet.FirstOrDefault(condition);

    public async Task<TEntity> FirstOrDefaultAsync()
        => await DbSet.FirstOrDefaultAsync();

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
        => await DbSet.FirstOrDefaultAsync(condition);

    public TEntity Single()
        => DbSet.Single();

    public TEntity Single(Expression<Func<TEntity, bool>> condition)
        => DbSet.Single(condition);

    public async Task<TEntity> SingleAsync()
        => await DbSet.SingleAsync();

    public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> condition)
        => await DbSet.SingleAsync(condition);

    public TEntity SingleOrDefault()
        => DbSet.SingleOrDefault();

    public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> condition)
        => DbSet.SingleOrDefault();

    public async Task<TEntity> SingleOrDefaultAsync()
        => await DbSet.SingleOrDefaultAsync();
        
    public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> condition)
        => await DbSet.SingleOrDefaultAsync(condition);
}