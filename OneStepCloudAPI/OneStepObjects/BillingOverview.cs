using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class BillingOverview
    {
        public string CurrentPeriod
        {
            get
            {
                return $"{PeriodStart.ToString(CurrentPeriodFormat)} - {PeriodEnd.ToString(CurrentPeriodFormat)}";
            }
            set
            {
                var dates = value.Split(new string[] { " - " }, StringSplitOptions.None);

                if (Regex.IsMatch(dates[0], @"^\d{4}-\d{2}-\d{2}$") && Regex.IsMatch(dates[1], @"^\d{4}-\d{2}-\d{2}$")) // yyyy-mm-dd
                {
                    PeriodStart = DateTime.ParseExact(dates[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    PeriodEnd = DateTime.ParseExact(dates[1], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                else if (Regex.IsMatch(dates[0], @"^\d{2}-\d{2}-\d{4}$") && Regex.IsMatch(dates[1], @"^\d{2}-\d{2}-\d{4}$")) // mm-dd-yyyy
                {
                    PeriodStart = DateTime.ParseExact(dates[0], "MM-dd-yyyy", CultureInfo.InvariantCulture);
                    PeriodEnd = DateTime.ParseExact(dates[1], "MM-dd-yyyy", CultureInfo.InvariantCulture);
                }
                else
                    throw new System.FormatException("OneStep servers returned invalid format date. Cannot parse billing period.");
            }
        }
        public string CurrentBalance { get; set; }
        public string CurrentCost { get; set; }
        public string ComputingCost { get; set; }
        public string StorageCost { get; set; }
        public string NetworkCost { get; set; }
        public string OtherCost { get; set; }
        public string DedicatedCost { get; set; }
        public decimal ComputingValue { get; set; }
        public decimal StorageValue { get; set; }
        public decimal NetworkValue { get; set; }
        public decimal OtherValue { get; set; }
        public decimal DedicatedValue { get; set; }
        public List<decimal> CostHistory { get; set; }


        [JsonIgnore]
        public DateTime PeriodStart { get; set; }
        [JsonIgnore]
        public DateTime PeriodEnd { get; set; }
        [JsonIgnore]
        private string CurrentPeriodFormat { get; set; } = "MM-dd-yyyy";
    }
}
