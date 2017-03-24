using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class NAT
    {
        public int Id { get; set; }
        public NetworkResourceStatus Status { get; set; }
        public string SourcePortRange { get; set; }
        public string DestinationPortRange { get; set; }
        public NetworkProtocol Protocol { get; set; }
    }
}
