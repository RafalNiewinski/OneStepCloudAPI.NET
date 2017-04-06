using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class Invitation
    {
        public int Id { get; set; }
        public string InvitationToken { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime InvitationExpiresAt { get; set; }

        public static implicit operator int(Invitation inv) { return inv.Id; }
    }
}
