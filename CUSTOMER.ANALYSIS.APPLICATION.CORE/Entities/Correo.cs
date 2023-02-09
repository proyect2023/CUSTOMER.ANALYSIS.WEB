using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities
{
    public partial class Correo
    {
        public long IdCorreo { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string Correos { get; set; }
        public string Copias { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public int? Tipo { get; set; }
        public int? Adjunto { get; set; }
        public string Error { get; set; }
        public int? Estado { get; set; }

        public int? IntentosEnvio { get; set; }
    }
}
