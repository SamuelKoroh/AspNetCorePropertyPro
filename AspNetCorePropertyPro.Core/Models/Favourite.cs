using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AspNetCorePropertyPro.Core.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]

        public Property Property { get; set; }
    }
}
