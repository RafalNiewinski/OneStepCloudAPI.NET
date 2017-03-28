using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
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
        public string Icon_tag { get; set; }
    }
}
