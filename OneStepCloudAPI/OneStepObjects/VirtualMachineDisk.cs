using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum VirtualMachineDiskStorageType
    {
        optimal,
        performance
    }

    public class VirtualMachineDisk
    {
        public int Id { get; set; }
        public VirtualMachineDiskStorageType StorageType { get; set; }
        public int StorageGb { get; set; }
        public bool Primary { get; set; }

        public static implicit operator int(VirtualMachineDisk disk) { return disk.Id; }
    }
}
