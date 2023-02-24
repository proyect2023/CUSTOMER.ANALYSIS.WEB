using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IDashboardAppService
    {
        MethodResponseDto Get();
        MethodResponseDto GetGraficoLineaComportamientoLineaVentasCompras(int anio);
        MethodResponseDto GetGraficoLineaPlanesRentabilidad(int anio);
    }
}
