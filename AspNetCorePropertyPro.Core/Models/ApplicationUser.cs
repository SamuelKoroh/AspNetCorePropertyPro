using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AspNetCorePropertyPro.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Properties = new Collection<Property>();
            Favourites = new Collection<Favourite>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public ICollection<Property> Properties { get; set; }
        public ICollection<Favourite> Favourites { get; set; }
    }
}
