using OneStepCloudAPI.REST;
using System.Threading.Tasks;

namespace OneStepCloudAPI.Managers
{
    public class VpnManager
    {
        readonly OSCRequestManager rm;

        public VpnManager(OSCRequestManager rm)
        {
            this.rm = rm;
        }

        public async Task ChangePassword(string currentPassword, string newPassword)
        {
            await rm.SendRequest("vpn/change_password", Method.POST, new
            {
                current_password = currentPassword,
                new_password = newPassword,
                new_password_confirmation = newPassword
            });
        }
    }
}
