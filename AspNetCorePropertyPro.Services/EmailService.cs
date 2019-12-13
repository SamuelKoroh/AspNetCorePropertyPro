using AspNetCorePropertyPro.Core.Configurations;
using AspNetCorePropertyPro.Core.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Services
{
    public class EmailService : IEmailService
    {
        private readonly SendGridSetting _sendGridSetting;

        public EmailService(SendGridSetting sendGridSetting)
        {
            _sendGridSetting = sendGridSetting;
        }
        public async Task SendMailAsync(string toAddress, string subject, string body)
        {
            var client = new SendGridClient(_sendGridSetting.ApiKey);
            var from = new EmailAddress("samuel.koroh@tarvostechnology.com", "Property Pro");
            var to = new EmailAddress(toAddress);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            await client.SendEmailAsync(msg);
            
        }
    }
}
