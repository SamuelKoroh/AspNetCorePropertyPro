using System;
using System.Collections.Generic;
using System.Text;

namespace AspNetCorePropertyPro.Configuration.Utils
{
    public interface IResponse
    {
        object Ok(object data);
        object Error(object error);
    }
    public class Response : IResponse
    {
        public object Error(object error)
        {
            return new { status = "error", error };
        }

        public object Ok(object data)
        {
            return new { status = "success", data };
        }
    }
}
