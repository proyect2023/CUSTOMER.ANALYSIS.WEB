using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.DomainServices
{
    public class ParametroDto
    {
        public ParametroDto()
        {

        }

        public ParametroDto(Parametros parametro)
        {
            Codigo= parametro.Codigo;
            Valor = parametro.Valor;
            Descripcion = parametro.Descripcion;
            EsEntero = false;
            EsDecimal = false;
            EsTextoLargo = false;

            if (int.TryParse(Valor, out int num))
            {
                EsEntero = true;
            }

            if (decimal.TryParse(Valor, out decimal dec) && EsEntero == false)
            {
                EsDecimal = true;
                Valor = Utilidades.DoubleToString_FrontCO(dec, 2);
            }
            
            if (Valor?.Length > 100)
            {
                EsTextoLargo = true;
            }

        }

        public string? Codigo { get; set; }
        public string? Valor { get; set; }
        public string? Descripcion { get; set; }
        public bool? EsEntero { get; set; }
        public bool? EsDecimal { get; set; }
        public bool? EsTextoLargo { get; set; }

        public string? Usuario { get; set; }
        public string? Ip { get; set; }
    }
}
