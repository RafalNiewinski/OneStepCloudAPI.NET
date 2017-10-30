using OneStepCloudAPI.OneStepObjects;
using OneStepCloudAPI.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Managers
{
    public class UsersManager
    {
        readonly IOSCRequestManager rm;

        public UsersManager(IOSCRequestManager rm)
        {
            this.rm = rm;
        }

        public async Task<List<User>> GetAll()
        {
            List<User> users = await rm.SendRequest<List<User>>("users");

            foreach (var user in users)
            {
                User details = await rm.SendRequest<User>($"users/{user.Id}");
                User permissions = await rm.SendRequest<User>($"users/{user.Id}/permissions");

                user.UserDetail = details.UserDetail;
                user.Permissions = permissions.Permissions;
            }

            return users;
        }

        public async Task<User> UpdateUserDetails(User user)
        {
            await rm.SendRequest($"users/{user.Id}", Method.PATCH, new { user_detail = user.UserDetail });

            var users = await GetAll();
            return users.Where(x => x.Id == user.Id).First();
        }

        public async Task<User> UpdatePermissions(User user)
        {
            await rm.SendRequest($"users/{user.Id}/permissions", Method.PUT, new { permissions = user.Permissions });

            var users = await GetAll();
            return users.Where(x => x.Id == user.Id).First();
        }

        public async Task<User> AddUserPermission(int user, UserPermission perm)
        {
            var users = await GetAll();
            User live = users.Where(x => x.Id == user).First();

            if (!live.Permissions.Contains(perm))
            {
                live.Permissions.Add(perm);
                live = await UpdatePermissions(live);
            }

            return live;
        }

        public async Task<User> RemoveUserPermission(int user, UserPermission perm)
        {
            var users = await GetAll();
            User live = users.Where(x => x.Id == user).First();

            if (live.Permissions.Contains(perm))
            {
                live.Permissions.Remove(perm);
                live = await UpdatePermissions(live);
            }

            return live;
        }

        public async Task<bool> EmailAvailable(string address)
        {
            try
            {
                string res = await rm.SendRequest("user/email_available", Method.POST, new { email = address });

                bool valid = Newtonsoft.Json.Linq.JObject.Parse(res).SelectToken("$.valid").ToObject<bool>();
                bool available = Newtonsoft.Json.Linq.JObject.Parse(res).SelectToken("$.available").ToObject<bool>();

                if (valid && available)
                    return true;

                return false;
            }
            catch (FormatException) { return false; }
        }

        public async Task<bool> UsernameAvailable(string username)
        {
            string res = await rm.SendRequest("user/username_available", Method.POST, new { username = username });

            return Newtonsoft.Json.Linq.JObject.Parse(res).SelectToken("$.valid").ToObject<bool>();
        }

        public Task<List<Invitation>> GetInvitations()
        {
            return rm.SendRequest<List<Invitation>>("invitation");
        }

        public async Task<int> SendInvitation(string email, string username)
        {
            return await rm.SendRequest<OSCID>("invitation", Method.POST, new { username = username, email = email });
        }

        public async Task CancelInvitation(Invitation invite)
        {
            await rm.SendRequest($"invitation/{invite.InvitationToken}", Method.DELETE, new { id = invite.InvitationToken });
        }

        public async Task DeleteUser(int user)
        {
            await rm.SendRequest($"users/{user}", Method.DELETE);
        }
    }
}
