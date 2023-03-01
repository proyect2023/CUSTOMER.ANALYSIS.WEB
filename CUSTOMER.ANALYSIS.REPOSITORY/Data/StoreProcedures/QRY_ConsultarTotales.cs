using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.REPOSITORY.Data
{
    public partial class EFContext
    {
        internal IEnumerable<ConsultarTotalesDto> ConsultarTotales(bool masVendidos = false, bool antiguos = false, int estadoClientePlan = 0, int sector = 0)
        {
            return ConsultarTotalesDto.FromSqlRaw("QRY_ConsultarTotales @p0, @p1, @p2, @p3", masVendidos, antiguos, estadoClientePlan, sector).ToList();
        }
    }
}
