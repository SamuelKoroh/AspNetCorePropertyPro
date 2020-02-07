using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Resources
{
    public class ChangePasswordResource
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
