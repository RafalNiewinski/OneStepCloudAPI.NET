using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class GroupLimits
    {
        public ResourceLimit Cpu { get; set; }
        public ResourceLimit MemoryMb { get; set; }
        public ResourceLimit StorageGb { get; set; }
        public ResourceLimit PublicNetworks { get; set; }
        public ResourceLimit Vms { get; set; }
    }
}
