using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Resources.Propertys
{
    public class PropertyImageResource
    {
        public int Id { get; set; }
        public string Public_Id { get; set; }
        public string Url { get; set; }
        public string Secure_Url { get; set; }
        public bool IsMain { get; set; }
    }
}
