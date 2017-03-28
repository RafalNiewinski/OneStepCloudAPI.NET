using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class BillingTimelineEntry
    {
        public DateTime Date { get; set; }
        public string Cost { get; set; }
        public string ComputingCost { get; set; }
        public string StorageCost { get; set; }

        public decimal GetNumericCost()
        {
            return decimal.Parse(Cost, NumberStyles.Currency, CultureInfo.InvariantCulture);
        }

        public decimal GetNumericComputingCost()
        {
            return decimal.Parse(ComputingCost, NumberStyles.Currency, CultureInfo.InvariantCulture);
        }

        public decimal GetNumericStorageCost()
        {
            return decimal.Parse(StorageCost, NumberStyles.Currency, CultureInfo.InvariantCulture);
        }
    }

    public class BillingTimeline
    {
        readonly List<BillingTimelineEntry> Entries;

        public BillingTimeline()
        {
            Entries = new List<BillingTimelineEntry>();
        }

        public BillingTimeline(List<BillingTimelineEntry> entries)
        {
            this.Entries = entries;
        }

        public void AddEntry(BillingTimelineEntry entry)
        {
            Entries.Add(entry);
        }

        public List<BillingTimelineEntry> GetEntries()
        {
            return Entries;
        }

        public KeyValuePair<DateTime, DateTime> TimelinePeriod()
        {
            var list = (from x in Entries select x.Date).OrderBy(x => x).ToList();
            return new KeyValuePair<DateTime, DateTime>(list.First(), list.Last());
        }
    }
}
