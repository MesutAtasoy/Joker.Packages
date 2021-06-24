using Joker.EventBus;

namespace Joker.Domain.DomainEvent
{
    public abstract class DomainEvent : Event, IDomainEvent
    {
    }
}