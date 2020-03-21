using Joker.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joker.EntityFrameworkCore.Configurations
{
    public abstract class BaseEntityModelConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntityModel
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.CreatedOnUtc).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            builder.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
        }
    }
}
