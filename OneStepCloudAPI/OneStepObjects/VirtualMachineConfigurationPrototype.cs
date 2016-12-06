using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class VirtualMachineConfigurationPrototype
    {
        /// <summary>
        /// Name can contain from 2 to 15 characters.
        /// Should start witch letter.
        /// Must match regex: ^[a-zA-Z]{1}[a-zA-Z0-9]{1,14}$
        /// </summary>
        public string VirtualMachineName { get; set; }
        public bool SshKey { get; set; }
        public int PrivateKeyId { get; set; }
    }
}
