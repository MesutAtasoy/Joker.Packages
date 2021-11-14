using DotNetCore.CAP;
using Joker.EventBus;

namespace Joker.CAP;

public class CAPEventDispatcher : IEventDispatcher
{
    private readonly ICapPublisher _publisher;

    public CAPEventDispatcher(ICapPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task Dispatch(object @event, string queueName, CancellationToken cancellationToken = default)
    {
        await _publisher.PublishAsync(queueName, @event, cancellationToken: cancellationToken);
    }

    public async Task Dispatch(IEvent @event, CancellationToken cancellationToken = default)
    {
        await _publisher.PublishAsync(@event.GetType().Name, @event, cancellationToken: cancellationToken);
    }

    public async Task Dispatch(IEnumerable<IEvent> events, CancellationToken cancellationToken = default)
    {
        await Task.WhenAll(events.Select(e => Dispatch(e, cancellationToken)));
    }
}