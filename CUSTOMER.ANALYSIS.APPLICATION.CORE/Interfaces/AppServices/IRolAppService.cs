using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IRolAppService
    {
        (List<RolAppResultDto>, string) ConsultarRoles(long idCompania);
        (bool, string) RegistrarRol(long IdUsuario, string nombre, bool estado);
        (bool, string) ActualizarRol(long idrol, string nombre, bool estado, long IdUsuario);
        (List<IdQueryDto>, string) ConsultarRolVentanas(short IdRol, string UsuarioAuditoria);
        (List<AsignarRolVentanaAppResultDto>, string) ConsultarVentanasActivas(string UsuarioAuditoria);
        (int?, string) Asignar(long idRol, string idPermisos, long usuarioAuditoria);
    }
}
