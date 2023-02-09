using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices
{
    public interface IRolQueryService
    {
        List<RolesQueryDto> ConsultaRolesXCompania(long idCompania, ref string mensaje);

        List<IdQueryDto> ConsultarRolVentanas(short IdRol, ref string mensaje);
    }
}
