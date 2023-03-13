using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.Services
{
    public class EscribirZipResponseDTO
    {
        public string RutaLocal { get; set; }
        public bool EstadoSolicitud { get; set; }
        public double LenghtBytes { get; set; } = 0;
        public double LenghtKb { get; set; } = 0;
        public double LenghtMb { get; set; } = 0;
        public bool TieneZip { get; set; } = false;
        public int CantidadArchivosZipeados { get; set; } = 0;
        public int CantidadArchivosNoZipeados { get; set; } = 0;
        public List<string> ArchivosNoZipeados { get; set; } = new List<string>();
        public bool TieneError { get; set; } = false;
        public string MensajeErrorPrincipal { get; set; }
        public string MensajeErrorSecundario { get; set; }
    }
}
