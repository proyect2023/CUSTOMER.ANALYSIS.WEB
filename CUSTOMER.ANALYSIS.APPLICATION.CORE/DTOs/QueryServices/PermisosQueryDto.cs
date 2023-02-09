using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices
{
    public class PermisosQueryDto
    {
        public long IdPermiso { get; set; }
        public long? Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public long? IdPadre { get; set; }
        public string NombreAbreviado { get; set; }
        public string Icono { get; set; }
    }
}
