using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class PrivateNetwork
    {
        public int Id { get; set; }
        public string IpAddress { get; set; }

        public static implicit operator int(PrivateNetwork net) { return net.Id; }
    }
}
