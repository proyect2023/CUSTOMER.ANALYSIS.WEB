using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
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
                TargetName = "Análisis Intercom",
                Credentials = new NetworkCredential(GlobalSettings.ConfiguracionMailUser, GlobalSettings.ConfiguracionMailPassword)
            };
        }

        public (bool, string) EnviarCorreo(MailDto mail, bool esHtlm = false)
        {
            try
            {
                string[] correos = mail.Correos.Split(";").ToArray();
                var attachments = new List<Attachment>();

                if (mail.Adjuntos != null && mail.Adjuntos.Any())
                {
                    foreach (var item in mail.Adjuntos)
                    {
                        var attachment = new Attachment(new MemoryStream(item.Adjunto), item.Nombre);
                        attachments.Add(attachment);
                    }
                }

                foreach (var item in correos)
                {
                    MailMessage email = new MailMessage(GlobalSettings.ConfiguracionMailUser, item, mail.Asunto, mail.Mensaje);
                    email.IsBodyHtml = esHtlm;
                    

                    foreach (var attachment in attachments)
                    {
                        email.Attachments.Add(attachment);
                    }

                    cliente.Send(email);
                }

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private void ProcesarCorreo(string correo, List<AdjuntoDto> adjuntoDtos)
        {

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
