using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Exceptions
{
    /// <summary>
    /// Thrown when OSC API server caused error
    /// </summary>
    public class ServerErrorException : OSCException
    {
        public HttpStatusCode Code { get; private set; }

        public ServerErrorException(HttpStatusCode code) : base() { Code = code; }
        public ServerErrorException(HttpStatusCode code, string message) : base(message) { Code = code; }
        public ServerErrorException(HttpStatusCode code, Exception inner) : base("", inner) { Code = code; }
        public ServerErrorException(HttpStatusCode code, string message, Exception inner) : base(message, inner) { Code = code; }
    }
}
