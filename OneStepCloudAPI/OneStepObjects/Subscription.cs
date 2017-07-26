using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class Subscription
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int InvoiceId { get; set; }
        public string Name { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool Expired { get; set; }
        public bool Active { get; set; }
        public DateTime PurchasedAt { get; set; }

        public static implicit operator int(Subscription s) { return s.Id; }
    }
}
