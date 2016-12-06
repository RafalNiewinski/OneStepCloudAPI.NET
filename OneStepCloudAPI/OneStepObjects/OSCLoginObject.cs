using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class OSCLoginObject
    {
        public string Email { get; set; }
        public string AuthenticationToken { get; set; }
        public bool Primary { get; set; }
    }
}
