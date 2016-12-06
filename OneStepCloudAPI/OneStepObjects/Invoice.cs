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
        public string Total { get; set; }
        public InvoiceStatus Status { get; set; }
        public string PeriodStart { get; set; }
        public string PeriodEnd { get; set; }
        public string IssuedAt { get; set; }
        public string PaidAt { get; set; }
        public string DueAt { get; set; }
    }
}
