using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class PrivateNetwork : PrivateNetworkMachine
    {
        public bool OneToOneNat { get; set; }
        public VirtualNetworkSummary VirtualNetwork { get; set; }
        public PublicNetworkReference OutboundNetwork { get; set; }
    }

    public class PrivateNetworkMachine : PrivateNetworkSummary
    {
        public MachineReference Machine { get; set; }
    }

    public class PrivateNetworkSummary
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }

        public static implicit operator int(PrivateNetworkSummary net) { return net.Id; }
    }
}
