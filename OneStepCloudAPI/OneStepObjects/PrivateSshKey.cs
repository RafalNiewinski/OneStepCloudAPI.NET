using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum PrivateSshKeyType
    {
        unknown,
        ssh,
        putty
    }

    public class PrivateSshKey
    {
        public string PrivateKey { get { return _key; } set { _key = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(value)); } }
        public PrivateSshKeyType KeyType { get; set; }
        public string Filename { get; set; }


        private string _key;
    }
}
