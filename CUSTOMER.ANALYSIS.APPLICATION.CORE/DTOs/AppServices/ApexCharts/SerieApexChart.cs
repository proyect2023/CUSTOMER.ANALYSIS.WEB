using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices.ApexCharts
{
    public class SerieApexChart
    {
        public string Name { get; set; }
        public List<decimal?> Data { get; set; }
        // public List<int> Data { get; set; }
    }
}
