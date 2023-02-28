using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities
{
    public class TipoIdentificacion
    {
        [Key]
        public long TipoIdentificacionId { get; set; }

        [Column(TypeName = "varchar(5)")]
        public string? Codigo { get; set; } 

        [Column(TypeName = "varchar(50)")]
        public string? Nombre { get; set; }
        public bool Estado { get; set; }
    }
}
