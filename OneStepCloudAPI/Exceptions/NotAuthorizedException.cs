using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Exceptions
{
    /// <summary>
    /// Thrown when user is not authorized but tryies to do authorization required action
    /// </summary>
    public class NotAuthorizedException : OSCException
    {
        public NotAuthorizedException() : base() { }
    }
}
