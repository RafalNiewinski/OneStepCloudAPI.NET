using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class BillingOverview
    {
        public string CurrentCost { get; set; }
        public string CurrentBalance { get; set; }
        public string CurrentPeriod
        {
            get
            {
                return String.Format("{0} - {1}", PeriodStart.ToString("MM-dd-yyyy"), PeriodEnd.ToString("MM-dd-yyyy"));
            }
            set
            {
                var dates = value.Split(new string[] { " - " }, StringSplitOptions.None);
                if (dates[0] != null) PeriodStart = DateTime.ParseExact(dates[0], "MM-dd-yyyy", CultureInfo.InvariantCulture);
                if (dates[1] != null) PeriodEnd = DateTime.ParseExact(dates[1], "MM-dd-yyyy", CultureInfo.InvariantCulture);
            }
        }
        public List<decimal> CostHistory { get; set; }


        [JsonIgnore]
        public DateTime PeriodStart { get; set; }
        [JsonIgnore]
        public DateTime PeriodEnd { get; set; }
    }
}
