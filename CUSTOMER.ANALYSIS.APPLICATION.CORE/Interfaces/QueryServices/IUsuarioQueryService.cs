
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices
{
    public interface IUsuarioQueryService
    {
        IEnumerable<UsuarioInternoQueryDto> ConsultaUsuarioInternoXCompania(long idCompania, ref string mensaje);

        //IEnumerable<UsuariosQueryDto> ObtenerUsuario(long idUsuario, ref string mensaje);

        //List<UsuariosQueryDto> ConsultarUsuario(ref string mensaje);
    }
}
