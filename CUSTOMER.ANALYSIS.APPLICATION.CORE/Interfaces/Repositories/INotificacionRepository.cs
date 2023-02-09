

using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories
{
    public interface INotificacionRepository : IRepository<Notificacion>
    {
        List<Notificacion> GetNotificaciones(long IdUsuario, bool Leidas);
        int AddNotificacionVacuna(string codigo, string enfermedad, DateTime fechaControl, int idControl);
    }
}
