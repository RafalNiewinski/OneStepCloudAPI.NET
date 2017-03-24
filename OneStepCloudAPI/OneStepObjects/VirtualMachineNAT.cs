using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class VirtualMachineNAT : NAT
    {
        public string PublicNetwork { get; set; }
        public string PrivateNetwork { get; set; }
    }
}
