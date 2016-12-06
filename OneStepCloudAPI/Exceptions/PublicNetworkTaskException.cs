using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Exceptions
{
    /// <summary>
    /// Thrown when public network job fails
    /// </summary>
    class PublicNetworkTaskException : OSCException
    {
        public PublicNetworkTaskException() : base() { }
        public PublicNetworkTaskException(string message) : base(message) { }
        public PublicNetworkTaskException(Exception inner) : base("", inner) { }
        public PublicNetworkTaskException(string message, Exception inner) : base(message, inner) { }
    }
}
