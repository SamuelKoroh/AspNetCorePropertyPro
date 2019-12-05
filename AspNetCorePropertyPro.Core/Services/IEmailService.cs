using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IEmailService
    {
        Task SendMailAsync(string mailBody, string toMailAddress);
    }
}
