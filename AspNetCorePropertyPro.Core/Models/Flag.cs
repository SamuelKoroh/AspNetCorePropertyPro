using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCorePropertyPro.Core.Models
{
    public class Flag
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Reason { get; set; }
       
    }
}
