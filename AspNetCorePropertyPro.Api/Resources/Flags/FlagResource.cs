
using AspNetCorePropertyPro.Api.Resources.Propertys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Resources.Flags
{
    public class FlagResource
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
        public PropertyResource Property { get; set; }
    }
}
