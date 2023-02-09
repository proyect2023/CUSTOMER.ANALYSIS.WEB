using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services
{
    public interface IEnvioMail
    {
        (bool, string) EnviarCorreo(string destinatario, string asunto, string mensaje, bool esHtlm = false);
        void EnviarCorreo(MailMessage message);
        Task EnviarCorreoAsync(MailMessage message);
    }
}
