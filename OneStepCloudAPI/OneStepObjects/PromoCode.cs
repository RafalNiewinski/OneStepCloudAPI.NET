using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class PromoCode
    {
        public string Code { get; set; }
        public string Amount { get; set; }
        public DateTime UsedAt { get; set; }

        public decimal GetNumericAmount()
        {
            return decimal.Parse(Amount, System.Globalization.NumberStyles.Currency, CultureInfo.InvariantCulture);
        }
    }
}
