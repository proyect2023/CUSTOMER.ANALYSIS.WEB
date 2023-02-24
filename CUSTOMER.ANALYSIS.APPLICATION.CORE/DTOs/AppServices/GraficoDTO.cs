using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices.ApexCharts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public class GraficoDTO
    {
        public List<SerieApexChart> Series { get; set; }
        public XAxisApexChart Xaxis { get; set; }
    }
}
