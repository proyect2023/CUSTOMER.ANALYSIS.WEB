
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices
{
    public interface IPortalQueryService
    {
        //CredencialQueryDto ConsultarCredencialXNit(string nitCompania, ref string mensaje);
        //List<ParametroQueryDto> ConsultarParametroXCodigoXNit(string nitCompania, string codigos, ref string mensaje);
        List<PermisosQueryDto> ConsultarVentanasActivas(ref string mensaje);
    }
}
