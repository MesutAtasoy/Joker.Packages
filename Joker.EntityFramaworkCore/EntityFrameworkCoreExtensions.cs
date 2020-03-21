using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Joker.Shared.Models.Interfaces;

namespace Joker.EntityFrameworkCore
{
    public static class EntityFrameworkCoreExtensions
    {
        public static void ApplyConfigurationsFromAssembly(this ModelBuilder modelBuilder)
        {
            bool Expression(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>);

            var types = Assembly.GetCallingAssembly().GetTypes().Where(type => type.GetInterfaces().Any(Expression)).ToList();

            foreach (var type in types)
            {
                dynamic configuration = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configuration);
            }
        }

        public static DbContextOptionsBuilder ConfigureWarningsAsErrors(this DbContextOptionsBuilder options)
        {
            return options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
        }

        public static void DetectChangesLazyLoading(this DbContext context, bool enabled)
        {
            context.ChangeTracker.AutoDetectChangesEnabled = enabled;
            context.ChangeTracker.LazyLoadingEnabled = enabled;
            context.ChangeTracker.QueryTrackingBehavior = enabled ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
        }

        public static IQueryable<T> Include<T>(this IQueryable<T> queryable, Expression<Func<T, object>>[] includes) where T : class
        {
            includes?.ToList().ForEach(include => queryable = queryable.Include(include));

            return queryable;
        }

        public static IQueryable<T> Queryable<T>(this DbContext context) where T : class
        {
            context.DetectChangesLazyLoading(false);

            return context.Set<T>().AsNoTracking();
        }

        public static void SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
        {
            SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
                .Invoke(null, new object[] { modelBuilder });
        }

        static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(EntityFrameworkCoreExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Single(t => t.IsGenericMethod && t.Name == "SetSoftDeleteFilter");

        public static void SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
            where TEntity : class, ISoftDeletable
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);
        }

        public static void ApplySoftDelete(this ModelBuilder modelBuilder)
        {
            bool Expression(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>);

            var types = Assembly.GetCallingAssembly().GetTypes().Where(type => type.GetInterfaces().Any(Expression)).ToList();

            foreach (var type in types)
            {
                dynamic configuration = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(configuration);
            }
        }

    }
}
