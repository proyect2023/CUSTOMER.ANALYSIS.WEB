using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IPermisoAppService
    {
        (List<PermisoAppResultDto>, string) ListarPermisos();
        (List<(long, string)>, string) VerPermisos();
        (PermisoAppResultDto, string) VerPermiso(string IdCifrado);
        (bool, string) AgregarPermiso(string NombreAbreviado, long? Codigo, string Icono, string Descripcion, string Url, long? IdPadre, long IdUsuario, string Ip);
        (bool, string) EditarPermiso(string IdCifrado, string NombreAbreviado, long? Codigo, string Icono, string Descripcion, string Url, long? IdPadre, long IdUsuario, string Ip);
        (bool, string) EliminarPermiso(string IdCifrado, long IdUsuario, string Ip);
    }
}
