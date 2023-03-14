using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IReporteService
    {
        MethodResponseDto DownloadReporte(List<ClientePlan> result, string FileName, DateTime fechaInicio, DateTime fechaFin, string format);
    }
}
