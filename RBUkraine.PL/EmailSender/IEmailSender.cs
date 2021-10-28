using System.Threading.Tasks;
using RBUkraine.PL.EmailSender.Models;

namespace RBUkraine.PL.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailModel emailModel);
    }
}
