using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace CUSTOMER.ANALYSIS.REPOSITORY.Data
{
    public partial class EFContext
    {
        internal IEnumerable<RolesQueryDto> ConsultaRolesXCompania(long idCompania)
        {
            return RolesQueryDto.FromSqlRaw("QRY_ConsultaSPRol @p0", idCompania).ToList();
        }
    }
}
