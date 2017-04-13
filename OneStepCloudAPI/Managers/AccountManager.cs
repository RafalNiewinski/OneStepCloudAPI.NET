using OneStepCloudAPI.OneStepObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Managers
{
    public class AccountManager
    {
        readonly OSCRequestManager rm;

        public AccountManager(OSCRequestManager rm)
        {
            this.rm = rm;
        }

        public Task<UserDetail> GetAccountDetails()
        {
            return rm.SendRequest<UserDetail>("user/profile");
        }

        public async Task UpdateAccountDetails(UserDetail details)
        {
            await rm.SendRequest("user/profile", RestSharp.Method.PATCH, new { user_detail = details });
        }

        public async Task ChangePassword(string oldpassword, string newpassword)
        {
            await rm.SendRequest("user/change_password", RestSharp.Method.POST, new { current_password = oldpassword, password = newpassword, password_confirmation = newpassword });
        }

        public Task<List<SshKey>> GetSshKeys()
        {
            return rm.SendRequest<List<SshKey>>("user/keys");
        }

        public async Task<int> AddSshKey(string name, string key)
        {
            return await rm.SendRequest<OSCID>("user/keys", RestSharp.Method.POST, new { name = name, key = key });
        }

        public async Task DeleteSshKey(int keyid)
        {
            await rm.SendRequest($"user/keys/{keyid}", RestSharp.Method.DELETE, new { id = keyid });
        }

        #region GROUP DELETE
        public Task<GroupDeleteChecklist> GroupDeleteChecklist()
        {
            return rm.SendRequest<GroupDeleteChecklist>("group/precheck_list");
        }

        public async Task GenerateEndingInvoice()
        {
            await rm.SendRequest("group/ending_invoice", RestSharp.Method.POST);
        }

        public async Task DeleteGroupPermanently()
        {
            await rm.SendRequest("group/goodbye", RestSharp.Method.POST);
        }
        #endregion
    }
}
