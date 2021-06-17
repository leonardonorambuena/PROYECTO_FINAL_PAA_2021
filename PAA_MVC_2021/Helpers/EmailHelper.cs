using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace PAA_MVC_2021.Helpers
{
    public class EmailHelper
    {
        private static readonly string SMTP_SERVER = ConfigurationManager.AppSettings["SMTP_SERVER"];
        private static readonly string SMTP_USER = ConfigurationManager.AppSettings["SMTP_USER"];
        private static readonly string SMTP_PASSWORD = ConfigurationManager.AppSettings["SMTP_PASSWORD"];
        private static readonly string SMTP_PORT = ConfigurationManager.AppSettings["SMTP_PORT"];
        private static readonly string FROM_EMAIL = ConfigurationManager.AppSettings["FROM_EMAIL"];

        public static bool Send(string emailTo, string subject, string body, out string msg)
        {
            try
            {
                MailMessage message = new MailMessage(FROM_EMAIL, emailTo);
                message.IsBodyHtml = true;
                message.Body = "<h1 style='color: blue'>Mi tienda</h1>";
                message.Body += $"<p>{body}</p>";
                message.Subject = subject;

                SmtpClient client = new SmtpClient(SMTP_SERVER, int.Parse(SMTP_PORT));
                client.Credentials = new NetworkCredential(SMTP_USER, SMTP_PASSWORD);

                client.Send(message);

                msg = "Email enviado correctamente";
                return true;
            } catch (Exception e)
            {
                // guardar en un log el error
                msg = e.Message;
                return false;
            }
        }




    }
}