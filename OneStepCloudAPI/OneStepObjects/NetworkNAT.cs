using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class NetworkNAT
    {
        public int Id { get; set; }
        public NetworkResourceStatus Status { get; set; }
        public string SourcePortRange { get; set; }
        public string DestinationPortRange { get; set; }
        public NetworkProtocol Protocol { get; set; }
        public int VirtualMachineId { get; set; }
        public string VirtualMachineNameTag { get; set; }
    }
}
