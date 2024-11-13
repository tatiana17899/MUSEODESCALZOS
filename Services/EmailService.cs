using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MUSEODESCALZOS.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string subject, string body, string toEmail)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var smtpClient = new SmtpClient(emailSettings["Host"])
            {
                Port = int.Parse(emailSettings["Port"]),
                Credentials = new NetworkCredential(emailSettings["FromEmail"], emailSettings["Password"]),
                EnableSsl = bool.Parse(emailSettings["EnableSsl"]),
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailSettings["FromEmail"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            // Se agrega la dirección de correo del cliente
            if (string.IsNullOrEmpty(toEmail))
            {
                throw new ArgumentNullException("El correo del destinatario no puede estar vacío.");
            }

            mailMessage.To.Add(toEmail); // Agregar el correo del cliente como destinatario

            

            smtpClient.Send(mailMessage); // Enviar el correo
        }
    }
}