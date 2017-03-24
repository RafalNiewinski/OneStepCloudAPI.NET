using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class OSCID
    {
        public int Id { get; set; }

        public OSCID() { }

        public OSCID(int id)
        {
            Id = id;
        }

        public static implicit operator int(OSCID id)
        {
            return id.Id;
        }
    }
}
