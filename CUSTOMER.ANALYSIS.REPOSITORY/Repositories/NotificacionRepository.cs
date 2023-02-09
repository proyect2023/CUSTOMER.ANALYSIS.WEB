using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories
{
    public class NotificacionRepository : Repository<Notificacion>, INotificacionRepository
    {
        public NotificacionRepository(EFContext context) : base(context)
        {
        }

        public List<Notificacion> GetNotificaciones(long IdUsuario, bool Leidas)
        {
            return _context.Notificaciones.Where(x => (x.IdUsuario == 0 || x.IdUsuario == IdUsuario) && x.Estado == 1 && x.EsNotificacionLeida == Leidas).ToList();
        }

        public int AddNotificacionVacuna(string codigo, string enfermedad, DateTime fechaControl, int idControl)
        {
            Notificacion notificacion = new Notificacion()
            {
                Titulo = "Alerta de vacuna",
                Mensaje = $"Cod: {codigo} próxima vacuna {Utilidades.FormatearFechaFormato(fechaControl)} para {enfermedad}",
                TipoNotificacion = TipoNotificacion.Notificacion,
                TipoCriticidad = 1,
                Estado = 1,
                FechaMostrar = fechaControl,
                IdControl = idControl,
                FechaCreacion = Utilidades.GetHoraActual(),
                IdUsuario = 0
            };

            _context.Notificaciones.Add(notificacion);
            return _context.SaveChanges();
        }
    }
}
