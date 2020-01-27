using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;

namespace PetFinder.Models
{
    public class Email
    {
        MailMessage m= new MailMessage();
        SmtpClient smtp = new SmtpClient();

        public bool EnviarCorreo(string para,string asunto, string mensaje)
        {
            try
            {
                m.From= new MailAddress("postmaster@petfinderarg.com");
                m.To.Add(para);
                m.Subject = asunto;
                m.Body = mensaje;
                m.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("mail.petfinderarg.com");

                NetworkCredential Credentials = new NetworkCredential("postmaster@petfinderarg.com", "Bruno-1977");
                
                smtp.UseDefaultCredentials = true;
                smtp.Port = 25;    //alternative port number is 8889
                smtp.Credentials = Credentials;
                smtp.EnableSsl = false;
                smtp.Send(m);
                
                //smtp.UseDefaultCredentials = false;
                
                return true;
            }
            catch(Exception e)
            {
                var error = e.Message;
                return false;
            }
        }

        public void ConfirmarCuenta(string para, string code)
        {
            var asunto = "Confirmacion de Cuenta";
                        
            var msj = "Para confirmar su cuenta haga click en el siguiente enlace \n" +
                "por favor http://petfinderarg.com/Home/Account?token=" + code;            
            
            EnviarCorreo(para, asunto, msj);
        }

        public void RestaurarClave(string para, string code)
        {
            var asunto = "Resturar Contraseña";

            var msj = "Para restaurar su conraseña haga click en el siguiente enlace \n" +
                "por fovar http://petfinderarg.com/Home/Recovery?token=" + code;
            EnviarCorreo(para, asunto, msj);
        }
    }
}
