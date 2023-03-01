using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string? Identificacion { get; set; }
        public string? RazonSocial { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
        public bool? Estado { get; set; }
        public TipoPersona? TipoPersona { get; set; }
        public string? TipoIdentificacion { get; set; }
        public string? Direccion { get; set; }
        public string? CorreoElectronico { get; set; }
        public string? Telefono { get; set; }
        public string? Ip { get; set; }
        public long? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public long? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public long? UsuarioEliminacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public virtual int? IdSector { get; set; }
        public virtual Sector? IdSectorNavigation { get; set; }
        public virtual ICollection<ClientePlan> ClientePlans { get; } = new List<ClientePlan>();

    }
}
