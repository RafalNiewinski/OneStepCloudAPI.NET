using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public abstract class BillingTimeline
    {
        public List<int> Timestamps { get; set; }

        public (DateTime TimelineStart, DateTime TimelineEnd) TimelinePeriod()
        {
            var times = Timestamps.OrderBy(x => x);
            return (DateTimeFromUnixTimestamp(times.First()), DateTimeFromUnixTimestamp(times.Last()));
        }

        protected DateTime DateTimeFromUnixTimestamp(int timestamp) => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(timestamp);
    }

    public class BillingTimelineOverview : BillingTimeline
    {
        public List<decimal> Values { get; set; }

        public List<(DateTime Time, decimal Value)> Entries
        {
            get
            {
                if (Timestamps.Count != Values.Count)
                    throw new InvalidOperationException("Timestamps and values count in timeline are not equal.");

                var entries = new List<(DateTime, decimal)>();
                for (int i = 0; i < Timestamps.Count; i++)
                {
                    entries.Add((DateTimeFromUnixTimestamp(Timestamps[i]), Values[i]));
                }

                return entries;
            }
        }
    }

    public class BillingTimelineVM : BillingTimeline
    {
        public List<decimal> Computing { get; set; }
        public List<decimal> Storage { get; set; }

        public List<(DateTime Time, decimal Computing, decimal Storage)> Entries
        {
            get
            {
                if (Timestamps.Count != Computing.Count || Computing.Count != Storage.Count)
                    throw new InvalidOperationException("Timestamps, computing and storage entry count in timeline are not equal.");

                var entries = new List<(DateTime, decimal, decimal)>();
                for (int i = 0; i < Timestamps.Count; i++)
                {
                    entries.Add((DateTimeFromUnixTimestamp(Timestamps[i]), Computing[i], Storage[i]));
                }

                return entries;
            }
        }
    }
}
