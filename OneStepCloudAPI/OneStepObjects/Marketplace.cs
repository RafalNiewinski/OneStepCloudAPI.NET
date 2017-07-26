using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum MarketplaceItemType
    {
        Unknown,
        licence,
        subscription
    }

    public enum MarketplaceItemPaymentType
    {
        Unknown,
        instant,
        end_of_month
    }

    public class MarketplaceItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MarketplaceItemType Type { get; set; }
        public string Price { get; set; }
        public MarketplaceItemPaymentType PaymentType { get; set; }
    }

    public class Marketplace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MarketplaceItem> Items { get; set; }
        public string IconTag { get; set; }
    }
}
