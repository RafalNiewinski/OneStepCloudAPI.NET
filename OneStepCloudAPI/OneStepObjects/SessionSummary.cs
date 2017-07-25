using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public enum UserPermission
    {
        unknown,
        virtual_machine_create,
        networking_manage,
        public_ip_request,
        public_ip_delete,
        billing_information_update,
        users_manage,
        user_invite,
        user_update,
        user_delete,
        user_permissions_update,
        public_key_create,
        public_key_delete,
        logs_view,
        billing_manage
    }

    public enum GroupStatus
    {
        unknown = 99,
        account_created = 0,
        account_verified = 1,
        payment_created = 2,
        payment_verified = 3,
        payment_overdue = 4,
        deleting_process = 5
    }

    public class SessionSummary
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<UserPermission> Permissions { get; set; }
        public bool NetworkBusy { get; set; }
        public GroupStatus GroupStatus { get; set; }
        public GroupLimits GroupLimits { get; set; }
        public InfrastructureSummary InfrastructureSummary { get; set; }

        public static implicit operator int(SessionSummary ss) { return ss.Id; }

        public bool ChackPermission(UserPermission perm)
        {
            if (!Permissions.Contains(perm))
                return false;

            return true;
        }
    }
}
