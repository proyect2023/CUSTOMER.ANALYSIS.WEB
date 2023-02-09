using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.AppServices
{
    public class NotificacionAppService : INotificacionAppService
    {
        private readonly INotificacionRepository notificacionRepository;

        public NotificacionAppService(INotificacionRepository notificacionRepository)
        {
            this.notificacionRepository = notificacionRepository;
        }

        public MethodResponseDto GetNotificaciones(long IdUsuario, bool Leidas = false)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                List<NotificacionAppResultDto> notificacionDtos = new List<NotificacionAppResultDto>();

                var result = notificacionRepository.GetNotificaciones(IdUsuario, Leidas);
                if (result != null)
                {
                    responseDto.Data = result.Select(c => new NotificacionAppResultDto
                    {
                        Id = c.IdNotificacion,
                        Titulo = c.Titulo,
                        Cuerpo = c.Mensaje,
                        TipoNotificacion = (TipoNotificacion)c.TipoNotificacion,
                        Fecha = c.FechaCreacion,
                        FechaMostrar = c.FechaMostrar,
                        NotificacionLeida = c.EsNotificacionLeida
                    }).ToList();

                    responseDto.Estado = true;
                }
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto MarcarLeido(long Id)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = notificacionRepository.Get(Id);
                result.EsNotificacionLeida = true;
                result.FechaNotificacionLeida = Utilities.Utilidades.GetHoraActual();

                notificacionRepository.Update(result);

                responseDto.Estado = notificacionRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }
    }
}
