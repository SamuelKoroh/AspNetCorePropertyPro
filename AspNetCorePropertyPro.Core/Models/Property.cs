using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AspNetCorePropertyPro.Core.Models
{
    public class Property
    {
        public Property()
        {
            Flags = new Collection<Flag>();
            Favourites = new Collection<Favourite>();
            PropertyImages = new Collection<PropertyImage>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public int TypeId { get; set; }
        public PropertyType Type { get; set; }
        public int DealTypeId { get; set; }
        public DealType DealType { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public ApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public ICollection<Flag> Flags { get; set; }
        public ICollection<Favourite> Favourites { get; set; }
        public ICollection<PropertyImage> PropertyImages { get; set; }
    }
}
