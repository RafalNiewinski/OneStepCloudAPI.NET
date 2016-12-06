﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class VirtualMachinePrototype
    {
        public int Cpu { get; set; }
        public int MemoryMb { get; set; }
        public int ProductId { get; set; }
        public List<VirtualMachineDisk> VirtualMachineDisks { get; set; }
    }
}
