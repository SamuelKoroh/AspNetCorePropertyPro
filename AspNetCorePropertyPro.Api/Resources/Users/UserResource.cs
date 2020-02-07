using AspNetCorePropertyPro.Api.Resources.Propertys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Resources.Users
{
    public class UserResource
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<PropertyResource> Properties { get; set; }
    }
}
