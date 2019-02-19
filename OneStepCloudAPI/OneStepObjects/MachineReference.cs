using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum MachineReferenceType
    {
        unknown,
        virtual_machine,
        dedicated_machine
    }

    public class MachineReference
    {
        public int Id { get; set; }
        public MachineReferenceType Type { get; set; }
        public string Name { get; set; }

        public static implicit operator int(MachineReference machine) => machine.Id;
    }
}
