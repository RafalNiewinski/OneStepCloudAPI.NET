using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum SystemType
    {
        Windows,
        Linux
    }

    public class OperatingSystemSummary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string VersionFull { get; set; }

        public static implicit operator int(OperatingSystemSummary os) { return os.Id; }
    }

    public class OperatingSystem : OperatingSystemSummary
    {
        public int StorageGb { get; set; }
        public SystemType SystemType { get; set; }
        public string IconTag { get; set; }
        public string ShortName { get; set; }
    }
}
