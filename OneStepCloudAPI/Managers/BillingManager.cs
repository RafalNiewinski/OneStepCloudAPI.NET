﻿using OneStepCloudAPI.OneStepObjects;
using OneStepCloudAPI.REST;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Managers
{
    public class BillingManager
    {
        readonly IOSCRequestManager rm;

        public BillingManager(IOSCRequestManager rm)
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

        public Task<BillingTimelineOverview> GetCostTimeline()
        {
            return rm.SendRequest<BillingTimelineOverview>("billing/cost_timeline");
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

        public Task<List<UsageReport>> GetUsageReports()
        {
            return rm.SendRequest<List<UsageReport>>("usage_reports");
        }

        public Task<byte[]> DownloadUsageReport(int id)
        {
            return rm.SendRequest<byte[]>($"usage_reports/{id}");
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

        public Task<BillingTimelineVM> GetVirtualMachineCostTimeline(int vmid)
        {
            return rm.SendRequest<BillingTimelineVM>($"billing/cost_timeline/{vmid}");
        }
    }
}
