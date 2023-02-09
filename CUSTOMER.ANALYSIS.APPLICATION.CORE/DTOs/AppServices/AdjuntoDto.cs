using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public class AdjuntoDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Archivo { get; set; }
        public string Ruta { get; set; }
        public string Identificador { get; set; }

        public byte[] Adjunto { get; set; }
        public long FileSize { get; set; }
    }
}
