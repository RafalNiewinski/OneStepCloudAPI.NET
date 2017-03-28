using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum Interval
    {
        hour,
        month
    }

    public class Resource
    {
        public int Id { get; set; }
        public string ResourceType { get; set; }
        public Interval Interval { get; set; }
        public double Price { get; set; }

        public static implicit operator int(Resource res) { return res.Id; }
    }
}
