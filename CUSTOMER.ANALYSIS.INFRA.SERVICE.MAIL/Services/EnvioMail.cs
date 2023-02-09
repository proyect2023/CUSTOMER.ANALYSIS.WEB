using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.INFRA.SERVICE.MAIL.Services
{
    public class EnvioMail : IEnvioMail
    {
        private readonly SmtpClient cliente;
        private MailMessage email;

        public EnvioMail()
        {
            cliente = new SmtpClient(GlobalSettings.ConfiguracionMailHost, GlobalSettings.ConfiguracionMailPort)
            {
                EnableSsl = GlobalSettings.ConfiguracionMailSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(GlobalSettings.ConfiguracionMailUser, GlobalSettings.ConfiguracionMailPassword)
            };
        }

        public (bool, string) EnviarCorreo(string destinatario, string asunto, string mensaje, bool esHtlm = false)
        {
            try
            {
                email = new MailMessage(GlobalSettings.ConfiguracionMailUser, destinatario, asunto, mensaje);
                email.IsBodyHtml = esHtlm;
                //email.Bcc = new MailAddressCollection();


                cliente.Send(email);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public void EnviarCorreo(MailMessage message)
        {
            cliente.Send(message);
        }

        public async Task EnviarCorreoAsync(MailMessage message)
        {
            await cliente.SendMailAsync(message);
        }
    }
}
