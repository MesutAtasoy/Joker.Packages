using DotNetCore.CAP;
using Joker.EventBus;

namespace Joker.CAP.IntegrationEvent;

public abstract class CAPIntegrationEventHandler<TEvent> : IIntegrationEventHandler<TEvent>, ICapSubscribe
    where TEvent : IIntegrationEvent
{
    public abstract Task Handle(TEvent @event);
}