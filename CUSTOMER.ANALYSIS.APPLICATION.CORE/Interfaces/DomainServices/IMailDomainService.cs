using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.DomainServices
{
    public interface IMailDomainService
    {
        MethodResponseDto EnviarMail(MailDto mailDto);
        MethodResponseDto EnviarMailRecuperarPassword(string Destinatario, string UsuarioNombreCompleto, string Password);
        MethodResponseDto EnviarMailBienvenida(string Destinatario, string UsuarioNombreCompleto, string Usuario, string Password);
        bool GuardarMail(MailDto mail);
    }
}
