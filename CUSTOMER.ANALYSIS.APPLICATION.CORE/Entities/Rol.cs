using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities
{
    public class Rol
    {
        public Rol()
        {
            RolPermiso = new HashSet<RolPermiso>();
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public long IdRol { get; set; }
        public string? Nombre { get; set; }
        public string? Ip { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public long? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public long? UsuarioModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public long? UsuarioEliminacion { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<RolPermiso> RolPermiso { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
