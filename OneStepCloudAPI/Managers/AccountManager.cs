using OneStepCloudAPI.OneStepObjects;
using OneStepCloudAPI.REST;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Managers
{
    public class AccountManager
    {
        readonly IOSCRequestManager rm;

        public AccountManager(IOSCRequestManager rm)
        {
            this.rm = rm;
        }

        public async Task<string> ActivateByCode(string code)
        {
            var res = await rm.SendRequest($"group/activate/by_promocode", Method.POST, new { code = code });
            return Newtonsoft.Json.Linq.JToken.Parse(res)["amount"].ToString();
        }

        public async Task ActivateByCard(BillingInformation billdata, CreditCardDetail cc)
        {
            await rm.SendRequest("group/activate/by_card", Method.POST, new { billing_information = billdata, credit_card_detail = cc });
        }

        public Task<UserDetail> GetAccountDetails()
        {
            return rm.SendRequest<UserDetail>("user/profile");
        }

        public async Task UpdateAccountDetails(UserDetail details)
        {
            await rm.SendRequest("user/profile", Method.PATCH, new { user_detail = details });
        }

        public async Task ChangePassword(string oldpassword, string newpassword)
        {
            await rm.SendRequest("user/change_password", Method.POST, new { current_password = oldpassword, password = newpassword, password_confirmation = newpassword });
        }

        public Task<List<SshKey>> GetSshKeys()
        {
            return rm.SendRequest<List<SshKey>>("user/keys");
        }

        public async Task<int> AddSshKey(string name, string key)
        {
            return await rm.SendRequest<OSCID>("user/keys", Method.POST, new { name = name, key = key });
        }

        public Task<PrivateSshKey> GenerateSshKey()
        {
            return rm.SendRequest<PrivateSshKey>("user/keys/generate?key_type=ssh");
        }

        public Task<PrivateSshKey> GeneratePuttyKey()
        {
            return rm.SendRequest<PrivateSshKey>("user/keys/generate?key_type=putty");
        }

        public async Task DeleteSshKey(int keyid)
        {
            await rm.SendRequest($"user/keys/{keyid}", Method.DELETE, new { id = keyid });
        }

        #region GROUP DELETE
        public Task<GroupDeleteChecklist> GroupDeleteChecklist()
        {
            return rm.SendRequest<GroupDeleteChecklist>("group/precheck");
        }

        public async Task GroupDeactivate()
        {
            await rm.SendRequest("group/deactivate", Method.POST);
        }

        public async Task DeleteGroupPermanently()
        {
            await rm.SendRequest("group/delete", Method.POST);
        }
        #endregion
    }
}
