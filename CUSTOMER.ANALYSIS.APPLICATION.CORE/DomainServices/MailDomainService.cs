using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.DomainServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DomainServices
{
    public class MailDomainService : IMailDomainService
    {
        private readonly IEnvioMail _envioMail;
        private readonly IMailRepository _mailRepository;
        private readonly IUtilidadRepository _utilidadRepository;

        public MailDomainService(IEnvioMail envioMail, IMailRepository mailRepository, IUtilidadRepository utilidadRepository)
        {
            _envioMail = envioMail;
            _mailRepository = mailRepository;
            _utilidadRepository = utilidadRepository;
        }

        public MethodResponseDto EnviarMailBienvenida(string Destinatario,
            string UsuarioNombreCompleto,
            string Usuario,
            string Password)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                string asunto = _utilidadRepository.GetParametro(DomainConstants.MAIL_BIENVENIDA_ASUNTO)?.Valor ?? "";
                string cuerpo = _utilidadRepository.GetParametro(DomainConstants.MAIL_BIENVENIDA_CUERPO)?.Valor ?? "";

                StringBuilder info = new StringBuilder();
                info.Append("<b>");
                info.Append("Usuario: ");
                info.Append("</b>");
                info.Append($"{Usuario}");
                info.Append("<br>");
                info.Append("<b>");
                info.Append($"Contraseña temporal: ");
                info.Append("</b>");
                info.Append($"{Password}");

                MailDto mail = new MailDto
                {
                    Tipo = TipoMail.Bienvenida,
                    Correos = Destinatario,
                    FechaIngreso = Utilidades.GetHoraActual(),
                    Asunto = BuildCuerpoCorreo(asunto),
                    Mensaje = BuildCuerpoCorreo(
                        CuerpoCorreo: cuerpo,
                        Username: UsuarioNombreCompleto,
                        Cuerpo: info.ToString())
                };

                return EnviarMail(mail);
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto EnviarMailRecuperarPassword(
            string Destinatario,
            string UsuarioNombreCompleto,
            string Password)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                string asunto = _utilidadRepository.GetParametro(DomainConstants.MAIL_RECUPERAR_PASSWORD_ASUNTO)?.Valor ?? "";
                string cuerpo = _utilidadRepository.GetParametro(DomainConstants.MAIL_RECUPERAR_PASSWORD_CUERPO)?.Valor ?? "";

                StringBuilder info = new StringBuilder();
                info.Append("<b>");
                info.Append("Usuario: ");
                info.Append("</b>");
                info.Append($"{UsuarioNombreCompleto}");
                info.Append("<br>");
                info.Append("<b>");
                info.Append($"Contraseña temporal: ");
                info.Append("</b>");
                info.Append($"{Password}");

                MailDto mail = new MailDto
                {
                    Tipo = TipoMail.RecuperarContraseña,
                    Correos = Destinatario,
                    FechaIngreso = Utilidades.GetHoraActual(),
                    Asunto = BuildCuerpoCorreo(asunto),
                    Mensaje = BuildCuerpoCorreo(
                        CuerpoCorreo: cuerpo,
                        Usuario_Nombre_Completo: UsuarioNombreCompleto,
                        Password_Temporal: info.ToString())
                };

                return EnviarMail(mail);

            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EnviarMail(MailDto mail)
        {
            MethodResponseDto responseDto = new MethodResponseDto();

            try
            {
                (bool result, string error) = _envioMail.EnviarCorreo(mail, true);
                if (result)
                {
                    mail.FechaEnvio = Utilidades.GetHoraActual();
                    mail.EstadosEnvioMail = EstadosEnvioMail.Enviado;
                }
                else
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), error);
                        responseDto.TieneErrores = true;
                        responseDto.CodigoError = DomainConstants.ERROR_ENVIAR_MAIL;
                        mail.Error = error;
                        mail.EstadosEnvioMail = EstadosEnvioMail.ErrorProceso;
                    }
                }

                responseDto.Estado = GuardarMail(mail); ;

            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public bool GuardarMail(MailDto mail)
        {
            var correo = new Entities.Correo
            {
                Asunto = mail.Asunto,
                Correos = mail.Correos,
                Copias = mail.Copias,
                FechaEnvio = mail.FechaEnvio,
                FechaIngreso = mail.FechaIngreso ?? Utilities.Utilidades.GetHoraActual(),
                Mensaje = mail.Mensaje,
                Tipo = ((int)mail.Tipo),
                Estado = ((int)mail.EstadosEnvioMail),
                Error = mail.Error,
                Adjunto = mail.Adjuntos != null && mail.Adjuntos.Any() ? 1 : 0
            };

            return _mailRepository.AddMail(correo);
        }

        private string BuildCuerpoCorreo(string CuerpoCorreo, 
            string Cuerpo = "", 
            string Username = "", 
            string Password_Temporal = "", 
            string Usuario_Nombre_Completo = ""
            )
        {
            if (string.IsNullOrEmpty(CuerpoCorreo)) CuerpoCorreo = "";

            CuerpoCorreo = CuerpoCorreo.Replace(DomainConstants.PARAM_CUERPO, Cuerpo ?? "");
            CuerpoCorreo = CuerpoCorreo.Replace(DomainConstants.PARAM_USERNAME, Username ?? "");
            CuerpoCorreo = CuerpoCorreo.Replace(DomainConstants.PARAM_PASSWORD_TEMPORAL, Password_Temporal ?? "");
            CuerpoCorreo = CuerpoCorreo.Replace(DomainConstants.PARAM_USUARIO_NOMBRE_COMPLETO, Usuario_Nombre_Completo ?? "");

            return CuerpoCorreo;
        }
    }
}
