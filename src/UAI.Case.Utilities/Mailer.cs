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


        private async static Task Send(MimeMessage message)
        {
            //sacar a config

            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587);
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            // Note: only needed if the SMTP server requires authentication
            client.Authenticate("uai.case.registros@gmail.com", "3l3c720n");

            await client.SendAsync(message);
            client.Disconnect(true);


        }

        public async static Task SendActivateRequest(Usuario usuario, string currentUrl)
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

                await Send(message);
                

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw new Exception(e.Message,e);
            }
        }


        public async static Task SendRegistrationRequest(string currentUrl, string registerToken, string mail)
        {
            ////TODO: Factorizar
            //try
            //{
            //    SmtpClient client = new SmtpClient();
            //    client.Port = 587;
            //    client.Host = "smtp.gmail.com";
            //    client.EnableSsl = true;
            //    client.Timeout = 10000;
            //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    client.UseDefaultCredentials = false;
            //    client.Credentials = new System.Net.NetworkCredential("uai.case.registros@gmail.com", "3l3c720n");



            //    MailMessage mm = new MailMessage(
            //        "uai.case.registros@gmail.com", mail, "UAI CASE - Registrarse",

            //        " haga click <a href='http://" + currentUrl + "?register=" + registerToken + "'>aquí</a> para registrarse");


            //    mm.BodyEncoding = UTF8Encoding.UTF8;
            //    mm.IsBodyHtml = true;
            //    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            //    client.Send(mm);
            //    return true;

            //}
            //catch (Exception e)
            //{
            //    Console.Write(e.Message);
            //    throw new Exception(e.Message, e);
            //}

            await Send(null);
        }

       
    }
}
