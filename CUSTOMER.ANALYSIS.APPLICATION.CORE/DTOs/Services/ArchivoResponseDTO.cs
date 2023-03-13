using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.Services
{
    public class ArchivoResponseDTO

    {
        public string NombreArchivo { get; set; }
        public string MensajeError { get; set; }
        public byte[] Archivo { get; set; }
    }
}
