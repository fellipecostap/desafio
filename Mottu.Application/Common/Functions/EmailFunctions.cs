using Desafio.Application.Common.Models;
using Desafio.Application.Common.Templates;
using Desafio.Application.Services.SendEmail.Commands.SendEmail.PreRegistration;
using System.Net;
using System.Net.Mail;

namespace Desafio.Application.Common.Functions
{
    public class EmailFunctions
    {
        public static bool SendEmailPreRegistration(SendEmailPreRegistrationCommand request, EmailSettings settings)
        {
            try
            {
                // Configura as informações do remetente
                string emailFrom = settings.EmailId;
                string passWord = settings.Password;

                // Configura as informações do destinatário
                string emailTo = request.UserEmail;

                // Configura as informações do servidor SMTP
                string smtp = settings.Host;
                int port = settings.Port;

                #region Body
                var emailTemplate = new EmailTemplateCreatePreRegistration();
                var bodyHtml = emailTemplate.EmailTemplateBodyHtml;
                var destName = request.NickName;
                var tokenNumber = request.UserToken;
                bodyHtml = string.Format(bodyHtml, destName, tokenNumber);
                #endregion

                // Cria um objeto de e-mail
                MailMessage email = new MailMessage();
                email.From = new MailAddress(emailFrom, "Desafio");
                email.To.Add(emailTo);
                email.Subject = "Seu código de confirmação";
                email.IsBodyHtml = true;
                email.Body = bodyHtml;

                // Cria um objeto de cliente SMTP
                SmtpClient client = new SmtpClient(smtp, port);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(emailFrom, passWord);
                client.Send(email);

                return true;
            }
            catch (Exception ex)
            {
                //throw new NotFoundException("Error:" + ex);
                //Log Exception Details
                var returnaooo = ex;
                return false;
            }
        }
    }
}

