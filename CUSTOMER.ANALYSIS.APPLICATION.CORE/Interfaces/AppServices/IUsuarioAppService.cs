
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Models;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IUsuarioAppService
    {
        MethodResponseDto CrearUsuario(UsuariosAppResultDto usuariosApp, long IdUsuarioCreacion, string Ip);
        MethodResponseDto EditarUsuario(UsuariosAppResultDto usuariosApp, long IdUsuarioCreacion, string Ip);
        MethodResponseDto ConsultarUsuarios();
        MethodResponseDto ObtenerUsuario(long idUsuario);
        (bool, string) EliminarUsuario(string IdCifrado, long IdUsuario, string Ip);
        MethodResponseDto ConsultarPerfil(long Id);
        MethodResponseDto ActualizarPerfil(PerfilModel model, long IdUsuarioCreacion, string Ip);
    }
}
