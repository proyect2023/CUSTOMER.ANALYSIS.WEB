using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            UsuarioRol = new HashSet<UsuarioRol>();
        }

        public long IdUsuario { get; set; }
        public string? Username { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Cedula { get; set; }
        public DateTime? FechaIncorporacion { get; set; }



        public DateTime? FechaUltimaConexion { get; set; }
        public int? IntentosFallidos { get; set; }
        public DateTime? FechaActualizarPassword { get; set; }
        public string? Ip { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public long? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public long? UsuarioModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public long? UsuarioEliminacion { get; set; }
        public EstadoUsuario Estado { get; set; }
        public string? Password { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? Foto { get; set; }
        public virtual ICollection<UsuarioRol> UsuarioRol { get; set; }
    }
}
