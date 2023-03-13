

using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.Services;

namespace AGRICOLA.LOLANDIA.APPLICATION.CORE.Interfaces.Services
{
    public interface IEscribirArchivoService
    {
        string EscribirZip(string pathDirectorio);
        EscribirZipResponseDTO ResponseEscribirZip(List<ArchivoResponseDTO> archivos, string rutaBase, string subdirectorio, string carpeta);
    }
}
