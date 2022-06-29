using System.Net.Mail;

namespace Epam.Rd.Application.Interfaces
{
    public interface IEmail
    {
        public void SendEmail(MailMessage mailMessage);
    }
}
