using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices
{
    public interface INotificacionAppService
    {
        MethodResponseDto GetNotificaciones(long IdUsuario, bool Leidas = false);
        MethodResponseDto MarcarLeido(long Id);
    }
}
