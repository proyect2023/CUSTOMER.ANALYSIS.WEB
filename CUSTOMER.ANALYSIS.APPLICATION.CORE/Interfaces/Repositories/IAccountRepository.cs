using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IAccountRepository : IRepository<Usuario>
    {
        bool RegistrarAccesoUsuario(AccesoUsuario acceso);
        List<VentanaLoginDto> ConsultarVentana(long IdUsuario, ref string mensaje);
    }
}
