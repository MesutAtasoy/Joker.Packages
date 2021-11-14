using Consul;
using Joker.Consul.Options;
using Microsoft.Extensions.Hosting;

namespace Joker.Consul;

public class ConsulHostedService : IHostedService
{
    private readonly IConsulClient _client;
    private readonly ServiceDiscoveryOption _config;

    public ConsulHostedService(IConsulClient client, ServiceDiscoveryOption config)
    {
        _client = client;
        _config = config;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var registration = new AgentServiceRegistration
        {
            ID = _config.ServiceId,
            Name = _config.ServiceName,
            Address = _config.Address,
            Port = _config.Port,
            Checks = _config.Endpoints.Select(x => new AgentServiceCheck
            {
                Interval = TimeSpan.FromSeconds(x.Internal ?? 10),
                Timeout = TimeSpan.FromSeconds(x.Timeout ?? 10),
                HTTP = x.Url
            }).ToArray(),
        };

        await _client.Agent.ServiceDeregister(registration.ID, cancellationToken);
        await _client.Agent.ServiceRegister(registration, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.Agent.ServiceDeregister(_config.ServiceId, cancellationToken);
    }
}