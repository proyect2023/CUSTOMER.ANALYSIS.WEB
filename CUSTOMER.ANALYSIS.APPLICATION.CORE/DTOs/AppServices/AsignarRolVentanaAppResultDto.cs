using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public sealed class AsignarRolVentanaAppResultDto
    {
        public AsignarRolVentanaAppResultDto()
        {
            Items = new List<AsignarRolVentanaAppResultDto>();
        }

        public long IdPermiso { get; set; }
        public string Nombre { get; set; }
        public List<AsignarRolVentanaAppResultDto> Items { get; set; }
    }
}
