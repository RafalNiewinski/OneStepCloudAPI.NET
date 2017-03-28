using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class VirtualMachineSnapshot
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public static implicit operator int(VirtualMachineSnapshot snap) { return snap.Id; }
    }
}
