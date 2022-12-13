using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
namespace Maistas.Areas.Identity.Data
{
    public class EmailService:IEmailSender
    {

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            await Execute(subject, message, toEmail);
        }

        public async Task Execute(string email, string subject, string htmlMessage)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("mantas.siupienius@gmail.com", "Example User");
            var to = new EmailAddress(email, "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = htmlMessage;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            
        }
    }
}
