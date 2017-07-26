using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class Licence
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int InvoiceId { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime PurchasedAt { get; set; }

        public static implicit operator int(Licence l) { return l.Id; }
    }
}
