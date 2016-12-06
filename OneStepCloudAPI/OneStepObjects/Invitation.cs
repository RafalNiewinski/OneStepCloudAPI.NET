using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class Invitation
    {
        public string InvitationToken { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string InvitationExpiresAt { get; set; }
    }
}
