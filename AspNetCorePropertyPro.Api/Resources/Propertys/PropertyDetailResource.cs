using System;
using System.Collections.Generic;

namespace AspNetCorePropertyPro.Api.Resources.Propertys
{
    public class PropertyDetailResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public ICollection<PropertyImageResource> Photos { get; set; }
    }
}
