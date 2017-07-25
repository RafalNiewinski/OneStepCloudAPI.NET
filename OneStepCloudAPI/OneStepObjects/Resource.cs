using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum Interval
    {
        unknown,
        hour,
        month
    }

    public class Resource
    {
        public string ResourceType { get; set; }
        public Interval Interval { get; set; }
        public double Price { get; set; }
    }
}
