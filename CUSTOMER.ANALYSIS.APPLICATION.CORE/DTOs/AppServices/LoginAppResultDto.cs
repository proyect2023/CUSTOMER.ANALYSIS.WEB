using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public class LoginAppResultDto
    {
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string CorreosAdministrador { get; set; }
        public long IdUsuario { get; set; }
        public List<Menu> Menu { get; set; }
        public string VentanasActivasConcat { get; set; }

        public bool ForzarCambioClave { get; set; }
        public string IPLogin { get; set; }
        public DateTime? FechaUltimaConexion { get; set; }

        public long Rol { get; set; }
        public string TimeZoneId { get; set; }
        public int CantidadNotificaciones { get; set; }
        public string Foto { get; set; }

    }
}
