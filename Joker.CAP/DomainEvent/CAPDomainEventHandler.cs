using System.Threading.Tasks;
using DotNetCore.CAP;
using Joker.Domain.DomainEvent;

namespace Joker.CAP.DomainEvent
{
    public abstract class CAPDomainEventHandler<TEvent> : IDomainEventHandler<TEvent>, ICapSubscribe where TEvent : IDomainEvent
    {
        public abstract Task Handle(TEvent domainEvent);
    }
}