using Joker.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Joker.EntityFrameworkCore
{
    public class EntityFrameworkCoreRelationalCommandRepository<T> : EntityFrameworkCoreCommandRepository<T>,IRelationalCommandRepository<T>
        where T : class
    {
        public EntityFrameworkCoreRelationalCommandRepository(DbContext context) : base(context)
        {
        }
    }
}
