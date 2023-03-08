using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices
{
    public interface IAnalisisQueryService
    {
        List<ConsultarTotalesDto> ConsultarTotales(string[] validaciones, string[] sectores, string[] planes);
    }
}
