using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class CreditCard
    {
        public int Id { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardType { get; set; }
        public bool IsDefault { get; set; }
    }
}
