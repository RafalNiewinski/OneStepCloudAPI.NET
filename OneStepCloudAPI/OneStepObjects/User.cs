using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneStepCloudAPI.OneStepObjects
{
    public class UserDetail
    {
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Language { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Primary { get; set; }
        public UserDetail UserDetail { get; set; }
        public List<UserPermission> Permissions { get; set; }


        public bool ChackPermission(UserPermission perm)
        {
            if (!Permissions.Contains(perm))
                return false;

            return true;
        }

        public static implicit operator int(User user) { return user.Id; }
    }
}
