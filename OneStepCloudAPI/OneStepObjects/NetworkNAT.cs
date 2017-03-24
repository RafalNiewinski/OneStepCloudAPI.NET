using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class NetworkNAT : NAT
    {
        public int VirtualMachineId { get; set; }
        public string VirtualMachineNameTag { get; set; }
    }
}
