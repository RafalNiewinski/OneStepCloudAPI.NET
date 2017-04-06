using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string DisplayType { get; set; }

        public static implicit operator int(PaymentMethod pm) { return pm.Id; }
    }

    public class Payment
    {
        public PaymentMethod PaymentMethod { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Amount { get; set; }
    }
}
