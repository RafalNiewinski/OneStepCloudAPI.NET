using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class SshKey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fingerprint { get; set; }
        public string CreatedAt { get; set; }
    }
}
