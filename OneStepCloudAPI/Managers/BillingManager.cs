using OneStepCloudAPI.OneStepObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Managers
{
    public class BillingManager
    {
        readonly OSCRequestManager rm;

        public BillingManager(OSCRequestManager rm)
        {
            this.rm = rm;
        }

        public async Task<BillingOverview> GetOverview()
        {
            return await rm.SendRequest<BillingOverview>("billing/overview");
        }

        public async Task<BillingTimeline> GetCostTimeline()
        {
            return new BillingTimeline(await rm.SendRequest<List<BillingTimelineEntry>>("billing/cost_timeline"));
        }

        public async Task<List<CreditCard>> GetCreditCards()
        {
            return await rm.SendRequest<List<CreditCard>>("credit_cards");
        }

        public async Task AddCreditCard(BillingInformation billdata, CreditCardDetail cc)
        {
            await rm.SendRequest("credit_cards", RestSharp.Method.POST, new { billing_information = billdata, credit_card_detail = cc });
        }

        public async Task MakeCreditCardDefault(int ccid)
        {
            await rm.SendRequest(String.Format("credit_cards/{0}", ccid), RestSharp.Method.PATCH, new { id = ccid });
        }

        public Task MakeCreditCardDefault(CreditCard cc)
        { return MakeCreditCardDefault(cc.Id); }

        public async Task DeleteCreditCard(int ccid)
        {
            await rm.SendRequest(String.Format("credit_cards/{0}", ccid), RestSharp.Method.DELETE, new { id = ccid });
        }

        public Task DeleteCreditCard(CreditCard cc)
        { return DeleteCreditCard(cc.Id); }

        public async Task<List<PromoCode>> GetPromoCodes()
        {
            return await rm.SendRequest<List<PromoCode>>("promocode");
        }

        public async Task ApplyPromoCode(string code)
        {
            await rm.SendRequest("promocode", RestSharp.Method.POST, new { code = code });
        }

        public async Task<List<Payment>> GetPaymentHistory()
        {
            return await rm.SendRequest<List<Payment>>("billing/payment_history");
        }

        public async Task<List<Invoice>> GetInvoices()
        {
            return await rm.SendRequest<List<Invoice>>("invoices");
        }

        public async Task<byte[]> DownloadInvoice(int id)
        {
            return await rm.SendRequest<byte[]>(String.Format("invoices/{0}.pdf", id));
        }

        public Task<byte[]> DownloadInvoice(Invoice i)
        {
            return DownloadInvoice(i.Id);
        }

        public async Task<List<VirtualMachineCost>> GetVirtualMachinesCosts()
        {
            return await rm.SendRequest<List<VirtualMachineCost>>("billing/detailed_summary");
        }

        public async Task<BillingTimeline> GetVirtualMachineCostTimeline(int vmid)
        {
            return new BillingTimeline(await rm.SendRequest<List<BillingTimelineEntry>>(String.Format("billing/cost_timeline/{0}", vmid)));
        }

        public Task<BillingTimeline> GetVirtualMachineCostTimeline(VirtualMachineSummary vm)
        {
            return GetVirtualMachineCostTimeline(vm.Id);
        }
    }
}
