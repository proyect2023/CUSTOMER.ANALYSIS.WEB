using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices
{
    public class ConsultarTotalesDto
    {
        public string? Identificacion { get; set; }
        public string? RazonSocial { get; set; }
        public DateTime? FechaContratacion { get; set; }
        public decimal? PagoMensual { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public string? Plan { get; set; }
        public string? TipoPlan { get; set; }
    }
}
