


using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services
{
    public interface IGemboxService
    {
        MethodResponseDto ConstruirReporte(List<ClientePlan> reportes, string NombreArchivo, string format, string ExtensionFormato = "xlsx");
    }
}
