using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum InvoiceItemType
    {
        Unknown,
        usage,
        sla,
        backup_recovery,
        managed_service,
        reconnection_charge,
        custom,
        licence,
        subscription
    }

    public class InvoiceItem
    {
        public string Name { get; set; }
        public InvoiceItemType Type { get; set; }
        public string Cost { get; set; }
    }
}
