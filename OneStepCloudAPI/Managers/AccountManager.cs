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

        public async Task<UserDetail> GetAccountDetails()
        {
            return await rm.SendRequest<UserDetail>("user/profile");
        }

        public async Task UpdateAccountDetails(UserDetail details)
        {
            await rm.SendRequest("user/profile", RestSharp.Method.PATCH, new { user_detail = details });
        }

        public async Task ChangePassword(string oldpassword, string newpassword)
        {
            await rm.SendRequest("user/change_password", RestSharp.Method.POST, new { current_password = oldpassword, password = newpassword, password_confirmation = newpassword });
        }

        public async Task<List<SshKey>> GetSshKeys()
        {
            return await rm.SendRequest<List<SshKey>>("user/keys");
        }

        public async Task<int> AddSshKey(string name, string key)
        {
            return await rm.SendRequest<OSCID>("user/keys", RestSharp.Method.POST, new { name = name, key = key });
        }

        public async Task DeleteSshKey(int keyid)
        {
            await rm.SendRequest(String.Format("user/keys/{0}", keyid), RestSharp.Method.DELETE, new { id = keyid });
        }
    }
}
