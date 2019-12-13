using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AspNetCorePropertyPro.Core.Response
{
    public class BaseResponse
    {
        [DefaultValue(false)]
        public bool Succeeded { get; set; }
        public object Message { get; set; }
        public object Data { get; set; }
    }
}
