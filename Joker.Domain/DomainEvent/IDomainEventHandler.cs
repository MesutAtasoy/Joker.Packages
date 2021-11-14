namespace Joker.Domain.DomainEvent
{
    public interface IDomainEventHandler<TEvent>  where TEvent : IDomainEvent
    {
        Task Handle(TEvent domainEvent);
    }
}