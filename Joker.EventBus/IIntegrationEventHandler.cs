using System.Threading.Tasks;

namespace Joker.EventBus
{
    public interface IIntegrationEventHandler<TEvent>  where TEvent : IIntegrationEvent
    {
        Task Handle(TEvent @event);
    }
}