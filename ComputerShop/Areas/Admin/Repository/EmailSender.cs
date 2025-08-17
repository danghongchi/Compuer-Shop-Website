using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ComputerShop.Areas.Admin.Repository
{
    public class EmailSender : IAppEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("chidang8198@gmail.com", "wkyyfsjopiluwxls")
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("chidang8198@gmail.com"),
                To = { email },
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            await client.SendMailAsync(mailMessage);
        }
    }
}
