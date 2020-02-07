using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Resources.Response
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Status = "error";
        }
        public string Status { get; set; }
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
