using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class BillingOverview
    {
        public string CurrentCost { get; set; }
        public string CurrentBalance { get; set; }
        public string CurrentPeriod { get; set; }
        public List<string> CostHistory { get; set; }
    }
}
