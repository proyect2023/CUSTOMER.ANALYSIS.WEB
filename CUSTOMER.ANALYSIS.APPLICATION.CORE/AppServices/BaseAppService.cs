using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.AppServices
{
    public abstract class BaseAppService
    {

        //protected internal BaseAppService(ILogInfraServices logInfraServices)
        //{
        //    _logInfraServices = logInfraServices;
        //}

        private static bool customCertValidation(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
        
        public static bool TestCorreo(string host, int puerto, string mail, string usuario, string clave, string mailPrueba, bool ssl, string asunto,ref string mensaje)
        {
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = host,
                    Port = puerto,
                    Credentials = new NetworkCredential(usuario, clave)
                };

                MailMessage miCorreo = new MailMessage
                {
                    From = new MailAddress(mail, asunto)
                };
                miCorreo.To.Add(mailPrueba);
                miCorreo.Subject = "Test servidor de correo";
                miCorreo.Body = "Prueba de configuración del servidor de correo";

                if (ssl == true)
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(customCertValidation);
                    smtp.EnableSsl = true;
                }

                smtp.Send(miCorreo);
                smtp.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                if (ex.InnerException != null)
                    mensaje = mensaje + " [" + ex.InnerException.Message + "]";
                return false;
            }
        }

        //public string RegistrarLogError(string origen, Exception ex, bool soloCodigo = false)
        //{
        //    if (soloCodigo)
        //    {
        //        return _logInfraServices.AddLog(origen, ex);
        //    }
        //    else
        //    {
        //        return ". Código de seguimiento: " + _logInfraServices.AddLog(origen, ex);
        //    }
        //}

    }
}
