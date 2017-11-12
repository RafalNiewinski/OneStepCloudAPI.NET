using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum MachineType
    {
        virtual_machine,
        dedicated_machine,
        unknown
    }

    public class NetworkNAT : NAT
    {
        public int MachineId { get; set; }
        public MachineType MachineType { get; set; }
        public string MachineNameTag { get; set; }
    }
}
