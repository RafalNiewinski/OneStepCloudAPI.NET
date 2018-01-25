using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class GroupDeleteChecklist
    {
        public bool PublicNetworks { get; set; }
        public bool VirtualMachines { get; set; }
        public bool DedicatedMachines { get; set; }
        public bool Nats { get; set; }
        public bool SubscriptionsCanceled { get; set; }
        public bool InvoicesPaid { get; set; }
        public bool EndingInvoiceIssued { get; set; }
        public bool EndingInvoicePaid { get; set; }
    }
}
