using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace APPAdminGroup.Helpers
{
    public class enviarCorreo
    {
        string urlDomain = "https://localhost:44327/";
        public void SendMail(string EmailDestino, string token)
        {
            string EmailOrigen = "bequejulian@gmail.com";
            string Contraseña = "pughhzlaffkbffhu";
            string url = urlDomain+"../Usuario/Confirmar/?token="+token;
            MailMessage omailMessage = new MailMessage(EmailOrigen, EmailDestino, "Confirmacion de Correo",
                "<p>Correo para confirmación de la cuenta<p/><br>" +
                "<a href='" + url + "'>Click para Confirmar el correo</a>");
            omailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

            oSmtpClient.Send(omailMessage);
            oSmtpClient.Dispose();
        }

        public void SendMailContraseña(string EmailDestino, string token)
        {
            string EmailOrigen = "bequejulian@gmail.com";
            string Contraseña = "pughhzlaffkbffhu";
            string url = urlDomain + "../Usuario/ContraseñaRecovery/?token=" + token;
            MailMessage omailMessage = new MailMessage(EmailOrigen, EmailDestino, "Recuperacion de contraseña",
                "<p>Correo para recuperar la contraseña<p/><br>" +
                "<a href='" + url + "'>Click para Confirmar el correo</a>");
            omailMessage.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;
            oSmtpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);

            oSmtpClient.Send(omailMessage);
            oSmtpClient.Dispose();
        }


    }
}