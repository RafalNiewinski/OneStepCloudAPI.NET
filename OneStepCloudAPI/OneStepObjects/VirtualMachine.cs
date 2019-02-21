using Newtonsoft.Json.Serialization;
using OneStepCloudAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class VirtualMachine : VirtualMachineSummary
    {
        public int StorageGb { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public ProductSummary Product { get; set; }
        public List<PrivateNetworkSummary> PrivateNetworks { get; set; }
        public List<VirtualMachineSnapshot> VirtualMachineSnapshots { get; set; }
        public List<VirtualMachineTask> VirtualMachineTasks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }


        public VirtualMachinePrototype GetPrototype()
        {
            return new VirtualMachinePrototype
            {
                Cpu = Cpu,
                MemoryMb = MemoryMb,
                ProductId = Product.Id,
                VirtualMachineDisks = VirtualMachineDisks
            };
        }
    }
}
