using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Resources.Propertys
{
    public class SaveEditPropertyResource
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public int TypeId { get; set; }
        public int DealTypeId { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
