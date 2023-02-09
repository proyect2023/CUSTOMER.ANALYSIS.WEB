using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities
{
    public class RolPermiso
    {
        public long Id { get; set; }
        public long? IdRol { get; set; }
        public long? IdPermiso { get; set; }
        public bool? Estado { get; set; }
        public long? UsuarioEliminacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public long? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }

        public virtual Permisos IdPermisoNavigation { get; set; }
        public virtual Rol IdRolNavigation { get; set; }
    }
}
