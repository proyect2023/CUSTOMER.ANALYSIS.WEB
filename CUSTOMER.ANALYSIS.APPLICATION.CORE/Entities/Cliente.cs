using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string? Identificacion { get; set; }
        public string? RazonSocial { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public bool? Estado { get; set; }
    }
}
