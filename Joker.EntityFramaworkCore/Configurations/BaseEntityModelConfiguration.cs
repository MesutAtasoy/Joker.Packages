using Joker.EntityFrameworkCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Joker.EntityFrameworkCore.Configurations
{
    public abstract class BaseEntityModelConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntityModel
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.Id).HasDefaultValue(Guid.NewGuid());
            builder.Property(e => e.CreatedOnUtc).HasDefaultValue(DateTime.UtcNow);
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.IsDeleted).HasDefaultValue(false);
        }
    }
}
