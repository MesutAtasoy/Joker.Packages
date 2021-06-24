using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Joker.Domain.Entities;
using Joker.EventBus;
using Joker.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Joker.EntityFrameworkCore.Domain
{
    public abstract class JokerDomainDbContext : JokerDbContext
    {
        private readonly IEventDispatcher _domainEventDispatcher;
        protected JokerDomainDbContext()
        {
        }

        protected JokerDomainDbContext(DbContextOptions options, IEventDispatcher domainEventDispatcher) : base(options)
        {
            Check.NotNull(options, nameof(options));
            
            _domainEventDispatcher = domainEventDispatcher;
        }
        
        public override int SaveChanges()
        {
            int result = base.SaveChanges();

            Task.Run(DispatchDomainEventsAsync);

            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            await DispatchDomainEventsAsync();

            return result;
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            int result = base.SaveChanges(acceptAllChangesOnSuccess);

            Task.Run(DispatchDomainEventsAsync);

            return result;
        }
        
        private async Task DispatchDomainEventsAsync()
        {
            if (_domainEventDispatcher == null)
                return;
            
            IEnumerable<DomainEntity> domainEntities = ChangeTracker.Entries()
                .Where(e => e.Entity is DomainEntity)
                .Select(e=> e.Entity)
                .Cast<DomainEntity>();

            foreach (DomainEntity domainEntity in domainEntities)
            {
                if (domainEntity.DomainEvents != null && domainEntity.DomainEvents.Any())
                {
                    await _domainEventDispatcher.Dispatch(domainEntity.DomainEvents);
                    domainEntity.ClearDomainEvents();
                }
            }
        }
    }
}