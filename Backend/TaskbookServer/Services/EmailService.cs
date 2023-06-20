using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using TaskbookServer.Models;

namespace TaskbookServer.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static async Task SendEmail(string email, string invitationtoken)
        {
                var apiKey = Environment.GetEnvironmentVariable("p1");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("alderskip@gmail.com", "Example User");
                var subject = "Sending with SendGrid is Fun";
                var to = new EmailAddress("ustin@sfedu.ru", "Example User");
                var plainTextContent = "and easy to do anywhere, even with C#";
                var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
            
        }
    }
}
