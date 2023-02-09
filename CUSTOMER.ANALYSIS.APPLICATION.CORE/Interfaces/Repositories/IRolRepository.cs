


using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities.StoreProcedures;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IRolRepository
    {
        bool ActualizaRol(SPRol rol, ref string mensaje);
        int? AsignarVentanas(long idRol, string idPermisos, long usuarioAuditoria, ref string mensaje);
        bool RegistrarRol(SPRol rol, ref string mensaje);
        List<Rol> ObtenerCodigoRol();
        List<Rol> GetRoles();
        List<Rol> GetRolesUsuario(long IdUsuario);

    }
}
