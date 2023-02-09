using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public class UsuarioRolAppResutlDto
    {
        public long Id { get; set; }
        public long? IdUsuario { get; set; }
        public long? IdRol { get; set; }
        public bool? Estado { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
