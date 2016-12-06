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
        public int CpuUsage { get; set; }
        public int MemoryMbUsage { get; set; }
        public string Username { get; set; }
        public Product Product { get; set; }
        public List<PrivateNetwork> PrivateNetworks { get; set; }
        public List<VirtualMachineSnapshot> VirtualMachineSnapshots { get; set; }
        public List<VirtualMachineTask> VirtualMachineTasks { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedAt { get; set; }


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
