using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Exceptions
{
    /// <summary>
    /// Thrown when requested resource could not be found
    /// </summary>
    public class ResourceNotFoundException : OSCException
    {
        public ResourceNotFoundException() : base() { }
    }
}
