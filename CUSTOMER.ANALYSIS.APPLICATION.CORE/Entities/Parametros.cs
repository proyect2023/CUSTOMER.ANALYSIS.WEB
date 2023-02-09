using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities
{
    public partial class Parametros
    {
        public string? Codigo { get; set; }
        public string? Valor { get; set; }
        public string? Descripcion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioEliminacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public bool Estado { get; set; }
    }
}
