using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices.ApexCharts
{
    public class GraficoPlan
    {
        public string NombreCorto { get; set; }
        public string NombreCompleto { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? ValorTotalDecimal { get; set; }
    }
}
