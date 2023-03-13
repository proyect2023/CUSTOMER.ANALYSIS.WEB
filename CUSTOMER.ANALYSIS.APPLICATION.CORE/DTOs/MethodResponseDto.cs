using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs
{
    public class MethodResponseDto
    {
        public bool Estado { get; set; }
        public bool TieneErrores { get; set; }
        public string Mensaje { get; set; }
        public string MensajeError { get; set; }
        public string CodigoError { get; set; }
        public dynamic Data { get; set; }
        public string MensajeAdicional { get; set; }
    }
}
