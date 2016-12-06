using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Exceptions
{
    /// <summary>
    /// Interface for all OSCAPILIB exceptions
    /// Should not be throwed directly
    /// </summary>
    public class OSCException : Exception
    {
        public OSCException() : base() { }
        public OSCException(string message) : base(message) { }
        public OSCException(string message, Exception inner) : base(message, inner) { }
    }
}
