using System.Net;
using System.Net.Mail;

namespace ChildVac.WebApi.Application.Utils
{
    /// <summary>
    ///     Used for sending emails though SMTP
    /// </summary>
    public static class SmtpServiceHelper
    {
        /// <summary>
        ///     Sends emails though SMTP
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public static void SendMail(string to, string subject, string body)
        {
            var fromAddress = new MailAddress("childvac@gmail.com");
            var toAddress = new MailAddress(to);
            const string fromPassword = "zaq12@!3";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
