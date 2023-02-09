using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IAccountAppService
    {
        (List<Menu>, string) LoadMenu(long IdUsuario);
        MethodResponseDto Login(string username, string password, string ip, bool DirActivo = false, string NomUsrDirAct = null, string GrupoUsuarioDirActivo = null);
        MethodResponseDto RecuperarPassword(string username, string email);
        MethodResponseDto CambiarPassword(long id, string ClaveActual, string ClaveNueva, string ClaveNuevaConfirma, bool EsCorreoRecuperacion);
        //MethodResponseDto ConsultarFinca();
        //MethodResponseDto ActualizarFinca(FincaModel model, long IdUsuarioCreacion, string Ip);
    }
}
