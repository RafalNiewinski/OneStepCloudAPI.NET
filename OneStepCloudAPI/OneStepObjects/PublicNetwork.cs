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
        unknown,
        idle,
        busy,
        error
    }

    public enum NetworkProtocol
    {
        unknown,
        tcp,
        udp,
        X1to1
    }

    public class PublicNetwork : PublicNetworkSummary
    {
        public List<NetworkNAT> Nats;
    }

    public class PublicNetworkSummary : PublicNetworkReference
    {
        public bool Primary { get; set; }
        public NetworkResourceStatus Status { get; set; }
        public int NatsCount { get; set; }
    }

    public class PublicNetworkReference
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }

        public static implicit operator int(PublicNetworkReference net) { return net.Id; }
    }
}
