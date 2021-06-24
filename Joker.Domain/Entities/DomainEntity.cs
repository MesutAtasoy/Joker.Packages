using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Joker.Domain.BusinessRule;
using Joker.Domain.DomainEvent;
using Joker.Exceptions;

namespace Joker.Domain.Entities
{
    public abstract class DomainEntity: Entity
    {
        private readonly List<IDomainEvent> _domainEvents;

        [IgnoreDataMember]
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        protected DomainEntity()
        {
            _domainEvents ??= new List<IDomainEvent>();
        }
        
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            Check.NotNull(domainEvent, nameof(domainEvent));

            _domainEvents.Add(domainEvent);
        }

        protected void AddOrUpdateDomainEvent(IDomainEvent domainEvent)
        {
            Check.NotNull(domainEvent, nameof(domainEvent));
            
            var @event = _domainEvents.FirstOrDefault(e => e.GetType() == domainEvent.GetType());
            if (@event != null)
            {
                RemoveDomainEvent(domainEvent);
            }
            
            AddDomainEvent(domainEvent);
        }

        protected void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            Check.NotNull(domainEvent, nameof(domainEvent));
            _domainEvents?.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
        
        protected static void CheckRule(IBusinessRule rule)
        {
            Check.NotNull(rule, nameof(rule));
            
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}