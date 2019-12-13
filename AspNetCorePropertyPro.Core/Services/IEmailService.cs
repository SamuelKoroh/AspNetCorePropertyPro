using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Core.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Send email message to user
        /// </summary>
        /// <param name="toAddress">The email address you are sending the mail to.</param>
        /// <param name="subject">The subject of the mail</param>
        /// <param name="body"> The content of the mail</param>
        /// <returns></returns>
        Task SendMailAsync(string toAddress, string subject, string body);
    }
}
