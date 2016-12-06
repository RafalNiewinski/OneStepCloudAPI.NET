using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class VirtualMachineCost
    {
        public int Id { get; set; }
        public int Cpu { get; set; }
        public int MemoryMb { get; set; }
        public int StorageGb { get; set; }
        public string NameTag { get; set; }
        public VirtualMachineStatus Status { get; set; }
        public string Cost { get; set; }
    }
}
