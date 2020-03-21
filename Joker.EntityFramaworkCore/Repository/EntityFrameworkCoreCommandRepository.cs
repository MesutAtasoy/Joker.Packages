using Joker.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Joker.EntityFrameworkCore
{
    public class EntityFrameworkCoreCommandRepository<T> : ICommandRepository<T> where T : class
    {
        public EntityFrameworkCoreCommandRepository(DbContext context)
        {
            Context = context;
        }

        private DbContext Context { get; }

        public void Add(T item)
        {
            Context.DetectChangesLazyLoading(true);

            Context.Set<T>().Add(item);
        }

        public Task AddAsync(T item)
        {
            Context.DetectChangesLazyLoading(true);

            return Context.Set<T>().AddAsync(item).AsTask();
        }

        public void AddRange(IEnumerable<T> items)
        {
            Context.DetectChangesLazyLoading(true);

            Context.Set<T>().AddRange(items);
        }

        public Task AddRangeAsync(IEnumerable<T> items)
        {
            Context.DetectChangesLazyLoading(true);

            return Context.Set<T>().AddRangeAsync(items);
        }

        public void Delete(object key)
        {
            Context.DetectChangesLazyLoading(true);

            var item = Context.Set<T>().Find(key);

            if (item == default) { return; }

            Context.Set<T>().Remove(item);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            Context.DetectChangesLazyLoading(true);

            var items = Context.Set<T>().Where(where);

            if (!items.Any()) { return; }

            Context.Set<T>().RemoveRange(items);
        }

        public Task DeleteAsync(object key)
        {
            Delete(key);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Expression<Func<T, bool>> where)
        {
            Delete(where);

            return Task.CompletedTask;
        }

        public void Update(object key, T item)
        {
            UpdateItem(key, item);
        }

        public Task UpdateAsync(object key, T item)
        {
            Update(key, item);

            return Task.CompletedTask;
        }

        public void UpdatePartial(object key, object item)
        {
            UpdateItem(key, item);
        }

        public Task UpdatePartialAsync(object key, object item)
        {
            UpdatePartial(key, item);

            return Task.CompletedTask;
        }

        private void UpdateItem(object key, object item)
        {
            Context.DetectChangesLazyLoading(true);

            var entity = Context.Set<T>().Find(key);

            var entry = Context.Entry(entity);

            entry.CurrentValues.SetValues(item);

            UpdateReferences(entry, item);
        }

        private static void UpdateReferences(EntityEntry<T> entry, object item)
        {
            foreach (var reference in entry.References)
            {
                var property = item.GetType().GetProperty(reference.Metadata.Name);

                if (property == default) { continue; }

                reference.CurrentValue = property.GetValue(item, default);
            }
        }
    }
}
