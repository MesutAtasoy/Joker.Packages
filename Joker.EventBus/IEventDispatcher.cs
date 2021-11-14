namespace Joker.EventBus;

public interface IEventDispatcher
{
    Task Dispatch(object @event, string queueName, CancellationToken cancellationToken = default);
    Task Dispatch(IEvent @event, CancellationToken cancellationToken = default);
    Task Dispatch(IEnumerable<IEvent> events, CancellationToken cancellationToken = default);
}