using Joker.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Joker.EntityFrameworkCore;

public abstract class JokerDbContext : DbContext
{
    protected JokerDbContext()
    {
    }

    protected JokerDbContext(DbContextOptions options) : base(options)
    {
        Check.NotNull(options, nameof(options));
    }
}