using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public class MailDto
    {
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaEnvio { get; set; }
        //public string NombreMostrar { get; set; }
        //public string CorreoMostrar { get; set; }
        public string Correos { get; set; }
        public string Copias { get; set; }
        public string CopiasOcultas { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public TipoMail Tipo { get; set; }
        public List<AdjuntoDto> Adjuntos { get; set; }
        public string Error { get; set; }
        public EstadosEnvioMail EstadosEnvioMail { get; set; } = EstadosEnvioMail.PendienteEnvio;
        public int? IntentosEnvio { get; set; }

    }
}
