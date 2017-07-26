using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum InvoiceStatus
    {
        unknown,
        unpaid,
        paid,
        overdue,
        ending_unpaid,
        ending_paid,
        pending
    }

    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string Total { get; set; }
        public InvoiceStatus Status { get; set; }
        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime PaidAt { get; set; }
        public DateTime DueAt { get; set; }

        public static implicit operator int(Invoice inv) { return inv.Id; }
    }
}
