using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities.StoreProcedures
{
    public class SPRol
    {
        public SPRol()
        {

        }

        public long IdUsuario { get; set; }
        public long IdRol { get; set; }
        public string Nombre { get; set; }
        //public string Descripcion { get; set; }
        public bool Estado { get; set; }
        //public string UsuarioAuditoria { get; set; }
    }
}
