using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RBUkraine.PL.EmailSender.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace RBUkraine.PL.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly string _sendGridApiKey;

        public EmailSender(
            IConfiguration configuration)
        {
            _sendGridApiKey = string.Join("", configuration["EmailSender:ApiKey"].Split("///"));
        }

        public Task SendEmailAsync(EmailModel emailModel)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress("rbukraine2021@gmail.com", "RBUkraine"),
                Subject = emailModel.Subject,
                PlainTextContent = emailModel.Message,
                HtmlContent = emailModel.Message
            };
            msg.AddTo(new EmailAddress(emailModel.Email));
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
