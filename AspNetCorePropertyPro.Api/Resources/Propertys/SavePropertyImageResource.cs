using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Resources.Propertys
{
    public class SavePropertyImageResource
    {
        public IFormFile File { get; set; }

    }
}
