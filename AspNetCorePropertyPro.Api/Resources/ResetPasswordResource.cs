using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Resources
{
    public class ResetPasswordResource
    {
        public string UserId { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
    }
}
