using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class CreditCardDetail
    {
        public string Number { get; set; }
        public string ExpirationDate { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
