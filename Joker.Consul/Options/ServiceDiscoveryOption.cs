using System.Collections.Generic;

namespace Joker.Consul.Options
{
    public class ServiceDiscoveryOption
    {
        public string ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string ConsulUrl { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public List<EndpointConfigurationOption> Endpoints { get; set; }
    }
}