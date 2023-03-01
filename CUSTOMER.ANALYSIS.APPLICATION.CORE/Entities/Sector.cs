using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities
{
    public class Sector
    {
        public int IdSector { get; set; }
        public string? Nombre { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
