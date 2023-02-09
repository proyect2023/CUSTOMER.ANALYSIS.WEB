using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities
{
    public partial class UsuarioRol
    {
        public long Id { get; set; }
        public long? IdUsuario { get; set; }
        public long? IdRol { get; set; }
        public bool? Estado { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
