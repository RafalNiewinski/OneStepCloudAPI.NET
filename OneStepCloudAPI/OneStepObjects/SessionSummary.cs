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
        billing_manage,
        marketplace_view,
        marketplace_manage,
        dedicated_machine_create,
        virtual_network_create,
        virtual_network_update,
        virtual_network_delete
    }

    public enum GroupStatus
    {
        unknown = 99,
        account_created = 0,
        account_verified = 1,
        payment_created = 2,
        payment_overdue = 3,
        promocode_created = 4,
        blocked_expired = 5,
        blocked_end_of_money = 6,
        blocked_no_payment = 7,
        deactivated_expired = 8,
        deactivated_end_of_money = 9,
        deactivated_no_payment = 10,
        deactivated_by_user = 11,
        deleted_by_user = 12
    }

    public class SessionSummary
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<UserPermission> Permissions { get; set; }
        public string Language { get; set; }
        public bool Primary { get; set; }
        public bool NetworkBusy { get; set; }
        public GroupStatus GroupStatus { get; set; }
        public GroupLimits GroupLimits { get; set; }
        public InfrastructureSummary InfrastructureSummary { get; set; }
        public InvoiceStatus? InvoiceAlert { get; set; }
        public int AccountExpirationDate { get; set; }
        public int AccountDeactivationDate { get; set; }

        public static implicit operator int(SessionSummary ss) { return ss.Id; }

        public bool ChackPermission(UserPermission perm)
        {
            if (!Permissions.Contains(perm))
                return false;

            return true;
        }
    }
}
