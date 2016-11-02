using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UAI.Case.Domain.Common;

namespace UAI.Case.Utilities
{
    public static class Mailer
    {

        

        private async static Task Send(MimeMessage message, IMailAccount mail)
        {
            //sacar a config

            SmtpClient client = new SmtpClient();
            client.Connect(mail.smtp, mail.port);
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            // Note: only needed if the SMTP server requires authentication
            client.Authenticate(mail.account, mail.password);

            await client.SendAsync(message);
            client.Disconnect(true);


        }

        public async static Task SendActivateRequest(Usuario usuario, string currentUrl,IMailAccount mail)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("UAI CASE - Registros", "uai.case.registros@gmail.com"));
                message.To.Add(new MailboxAddress(String.Format("{0} {1}",usuario.Nombre,usuario.Apellido), usuario.Mail));
                message.Subject = "UAI CASE -Activar cuenta";

                var builder = new BodyBuilder();
                builder.HtmlBody = String.Format(@"Su usuario es: {0}<br/>
                        Su passwod temporal es: {1}<br/>
                        haga click <a href='http://{2}?id={3}&activate={4}'>aquí</a> para activar su cuenta ",
                        usuario.Mail, usuario.Password, currentUrl, usuario.Id, UAI.Case.Security.Cryptography.MD5Hash(usuario.RegisterToken));

                message.Body = builder.ToMessageBody();

                await Send(message,mail);
                

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw new Exception(e.Message,e);
            }
        }


        public async static Task SendRegistrationRequest(string currentUrl, string registerToken, string mail, IMailAccount account)
        {
           

            await Send(null, account);
        }

       
    }
}
