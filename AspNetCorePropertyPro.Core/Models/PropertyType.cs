using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AspNetCorePropertyPro.Core.Models
{
    public class PropertyType
    {
        public PropertyType()
        {
            Properties = new Collection<Property>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Property> Properties { get; set; }

    }
}
