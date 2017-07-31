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
        public string CurrentPeriod
        {
            get
            {
                return $"{PeriodStart.ToString("MM-dd-yyyy")} - {PeriodEnd.ToString("MM-dd-yyyy")}";
            }
            set
            {
                var dates = value.Split(new string[] { " - " }, StringSplitOptions.None);
                if (dates[0] != null) PeriodStart = DateTime.ParseExact(dates[0], "MM-dd-yyyy", CultureInfo.InvariantCulture);
                if (dates[1] != null) PeriodEnd = DateTime.ParseExact(dates[1], "MM-dd-yyyy", CultureInfo.InvariantCulture);
            }
        }
        public string CurrentBalance { get; set; }
        public string CurrentCost { get; set; }
        public string ComputingCost { get; set; }
        public string StorageCost { get; set; }
        public string NetworkCost { get; set; }
        public string OtherCost { get; set; }
        public decimal ComputingValue { get; set; }
        public decimal StorageValue { get; set; }
        public decimal NetworkValue { get; set; }
        public decimal OtherValue { get; set; }
        public List<decimal> CostHistory { get; set; }


        [JsonIgnore]
        public DateTime PeriodStart { get; set; }
        [JsonIgnore]
        public DateTime PeriodEnd { get; set; }
    }
}
