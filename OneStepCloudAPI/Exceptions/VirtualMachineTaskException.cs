using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Exceptions
{
    /// <summary>
    /// Thrown when vm operation fails
    /// </summary>
    public class VirtualMachineTaskException : OSCException
    {
        public VirtualMachineTaskException() : base() { }
        public VirtualMachineTaskException(string message) : base(message) { }
        public VirtualMachineTaskException(Exception inner) : base("", inner) { }
        public VirtualMachineTaskException(string message, Exception inner) : base(message, inner) { }
    }
}
