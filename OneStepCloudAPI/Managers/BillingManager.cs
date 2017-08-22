using OneStepCloudAPI.OneStepObjects;
using OneStepCloudAPI.REST;
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

        public Task<BillingOverview> GetOverview()
        {
            return rm.SendRequest<BillingOverview>("billing/overview");
        }

        public Task<List<InvoiceItem>> GetCurrentInvoiceItems()
        {
            return rm.SendRequest<List<InvoiceItem>>("billing/current_invoice");
        }

        public async Task<BillingTimeline> GetCostTimeline()
        {
            return new BillingTimeline(await rm.SendRequest<List<BillingTimelineEntry>>("billing/cost_timeline"));
        }

        public Task<List<CreditCard>> GetCreditCards()
        {
            return rm.SendRequest<List<CreditCard>>("credit_cards");
        }

        public async Task AddCreditCard(BillingInformation billdata, CreditCardDetail cc)
        {
            await rm.SendRequest("credit_cards", Method.POST, new { billing_information = billdata, credit_card_detail = cc });
        }

        public async Task MakeCreditCardDefault(int ccid)
        {
            await rm.SendRequest($"credit_cards/{ccid}", Method.PATCH, new { id = ccid });
        }

        public async Task DeleteCreditCard(int ccid)
        {
            await rm.SendRequest($"credit_cards/{ccid}", Method.DELETE, new { id = ccid });
        }

        public Task<List<PromoCode>> GetPromoCodes()
        {
            return rm.SendRequest<List<PromoCode>>("promocode");
        }

        public async Task ApplyPromoCode(string code)
        {
            await rm.SendRequest("promocode", Method.POST, new { code = code });
        }

        public Task<List<Payment>> GetPaymentHistory()
        {
            return rm.SendRequest<List<Payment>>("billing/payment_history");
        }

        public Task<List<Invoice>> GetInvoices()
        {
            return rm.SendRequest<List<Invoice>>("invoices");
        }

        public Task<byte[]> DownloadInvoice(int id)
        {
            return rm.SendRequest<byte[]>($"invoices/{id}.pdf");
        }

        public async Task PayInvoice(int id)
        {
            await rm.SendRequest($"invoices/{id}", Method.PATCH, new { id = id });
        }

        public Task<List<VirtualMachineCost>> GetVirtualMachinesCosts()
        {
            return rm.SendRequest<List<VirtualMachineCost>>("billing/detailed_summary");
        }

        public async Task<BillingTimeline> GetVirtualMachineCostTimeline(int vmid)
        {
            return new BillingTimeline(await rm.SendRequest<List<BillingTimelineEntry>>($"billing/cost_timeline/{vmid}"));
        }
    }
}
