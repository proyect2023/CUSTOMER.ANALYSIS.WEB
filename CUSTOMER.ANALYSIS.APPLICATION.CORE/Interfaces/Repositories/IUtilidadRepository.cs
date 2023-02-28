using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IUtilidadRepository
    {
        void InicializarDb();
        List<Parametros> GetParametros();
        Parametros? GetParametro(string Codigo);
        int ActualizarParametro(Parametros parametro);
        List<Rol> GetRolesPrincipales();
        bool AddRol(int Id, string Nombre);
        void AgregarUsuarioAdministrador();
        List<TipoIdentificacion> GetTipoIdentificaciones();
        List<TipoPlan> GetTiposPlan();
    }
}
    