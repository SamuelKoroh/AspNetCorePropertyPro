using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Resources.Response
{
    public class ErrorModel
    {
        public string FieldName { get; set; }
        public string Message { get; set; }
    }
}
