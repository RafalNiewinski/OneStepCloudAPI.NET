using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum NetworkResourceStatus
    {
        idle,
        busy,
        error
    }

    public enum NetworkProtocol
    {
        tcp,
        udp
    }

    public class PublicNetwork : PublicNetworkSummary
    {
        public List<NetworkNAT> Nats;
    }

    public class PublicNetworkSummary
    {
        public int Id { get; set; }
        public bool Primary { get; set; }
        public NetworkResourceStatus Status { get; set; }
        public string IpAddress { get; set; }
        public int NatsCount { get; set; }

        public static implicit operator int(PublicNetworkSummary net) { return net.Id; }
    }
}
