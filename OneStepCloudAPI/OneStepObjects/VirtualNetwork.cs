using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class VirtualNetwork : VirtualNetworkSummary
    {
        public bool Primary { get; set; }
        public int PublicNetworkId { get; set; }

        public List<PrivateNetworkMachine> PrivateNetworks { get; set; }
    }

    public class VirtualNetworkSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Network { get; set; }

        public static implicit operator int(VirtualNetworkSummary net) => net.Id;
    }
}
