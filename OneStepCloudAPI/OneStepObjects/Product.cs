using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class ProductPrices
    {
        public decimal Cpu { get; set; }
        public decimal MemoryMb { get; set; }
        public decimal HddOptimal { get; set; }
        public decimal HddPerformance { get; set; }
    }

    public class ProductSummary
    {
        public int Id { get; set; }
        public int MinimumCpu { get; set; }
        public int MaximumCpu { get; set; }
        public int MinimumMemoryMb { get; set; }
        public int MaximumMemoryMb { get; set; }
        public int MinimumStorageGb { get; set; }
        public int MaximumStorageGb { get; set; }
        public int MaximumAdditionalDisks { get; set; }
        public string CpuLabels { get; set; }
        public string MemoryLabels { get; set; }
        public string StorageLabels { get; set; }

        public static implicit operator int(ProductSummary prod) { return prod.Id; }
    }

    public class Product : ProductSummary
    {
        public OperatingSystem OperatingSystem { get; set; }
        public List<Resource> Resources { get; set; }
        public string Name { get; set; }
        public string IconTag { get; set; }
        public ProductPrices Prices { get; set; }


        #region VM PROTOTYPES

        public VirtualMachinePrototype GetVirtualMachinePrototype()
        {
            return new VirtualMachinePrototype
            {
                Cpu = MinimumCpu,
                MemoryMb = MinimumMemoryMb,
                ProductId = Id,
                VirtualMachineDisks = new List<VirtualMachineDisk> { new VirtualMachineDisk { Primary = true, StorageGb = OperatingSystem.StorageGb, StorageType = VirtualMachineDiskStorageType.optimal } }
            };
        }

        public VirtualMachinePrototype GetVirtualMachinePrototype(int cpu, int mem, VirtualMachineDiskStorageType disksType = VirtualMachineDiskStorageType.optimal, List<VirtualMachineDisk> additionalDisks = null)
        {
            var disks = new List<VirtualMachineDisk> { new VirtualMachineDisk { Primary = true, StorageGb = OperatingSystem.StorageGb, StorageType = disksType} };

            if (additionalDisks != null)
            {
                if (additionalDisks.Count > MaximumAdditionalDisks)
                    additionalDisks = additionalDisks.Take(MaximumAdditionalDisks).ToList();

                additionalDisks.ForEach(x =>
                {
                    x.Id = 0;
                    x.Primary = false;
                    x.StorageGb = (x.StorageGb < MinimumStorageGb) ? MinimumStorageGb : (x.StorageGb > MaximumStorageGb) ? MaximumStorageGb : x.StorageGb;
                    x.StorageType = disksType;
                });

                disks.AddRange(additionalDisks);
            }


            return new VirtualMachinePrototype
            {
                Cpu = (cpu < MinimumCpu) ? MinimumCpu : (cpu > MaximumCpu) ? MaximumCpu : cpu,
                MemoryMb = (mem < MinimumMemoryMb) ? MinimumMemoryMb : (mem > MaximumMemoryMb) ? MaximumMemoryMb : mem,
                ProductId = Id,
                VirtualMachineDisks = disks
            };
        }

        #endregion
    }
}
