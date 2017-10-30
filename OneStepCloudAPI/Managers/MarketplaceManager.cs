using OneStepCloudAPI.OneStepObjects;
using OneStepCloudAPI.REST;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Managers
{
    public class MarketplaceManager
    {
        readonly IOSCRequestManager rm;

        public MarketplaceManager(IOSCRequestManager rm)
        {
            this.rm = rm;
        }

        public Task<List<Marketplace>> GetMarketplaceProducts()
        {
            return rm.SendRequest<List<Marketplace>>("marketplace");
        }

        public Task<List<Licence>> GetPurchasedLicences()
        {
            return rm.SendRequest<List<Licence>>("licences");
        }

        public async Task<int> PurchaseLicence(int id)
        {
            return await rm.SendRequest<OSCID>($"licences/{id}", Method.PATCH);
        }

        public Task<List<Subscription>> GetPurchasedSubsctiptions()
        {
            return rm.SendRequest<List<Subscription>>("subscriptions");
        }

        public async Task<int> PurchaseSubscription(int id)
        {
            return await rm.SendRequest<OSCID>($"subscriptions/{id}", Method.PATCH);
        }

        public async Task CancelSubscription(int id)
        {
            await rm.SendRequest($"subscriptions/{id}", Method.DELETE);
        }
    }
}
