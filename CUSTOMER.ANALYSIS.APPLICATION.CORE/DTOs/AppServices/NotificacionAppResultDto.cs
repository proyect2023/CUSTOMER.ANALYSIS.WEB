using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public class NotificacionAppResultDto
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public TipoNotificacion TipoNotificacion { get; set; }
        public DateTime Fecha { get; set; }
        public bool NotificacionLeida { get; set; }
        public DateTime? FechaMostrar { get; set; }
    }
}
