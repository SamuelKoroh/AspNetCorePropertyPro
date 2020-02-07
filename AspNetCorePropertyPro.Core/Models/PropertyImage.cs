using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AspNetCorePropertyPro.Core.Models
{
    public class PropertyImage
    {
        public int Id { get; set; }
        public string Public_Id { get; set; }
        public string Url { get; set; }
        public string Secure_Url { get; set; }
        public bool IsMain { get; set; }
        public Property Property { get; set; }
        public int PropertyId { get; set; }
    }
}
