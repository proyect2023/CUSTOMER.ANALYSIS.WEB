using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities
{
    public partial class Permisos
    {
        public Permisos()
        {
            RolPermiso = new HashSet<RolPermiso>();
        }

        public long IdPermiso { get; set; }
        public long? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public string? Url { get; set; }
        public string? Ip { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public long? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public long? UsuarioModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public long? UsuarioEliminacion { get; set; }
        public int? Estado { get; set; }
        public long? IdPadre { get; set; }
        public string? NombreAbreviado { get; set; }
        public string? Icono { get; set; }
        public int? Orden { get; set; }

        public virtual ICollection<RolPermiso> RolPermiso { get; set; }
    }
}
