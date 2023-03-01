using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Models
{
    public class ClienteModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public TipoPersona TipoPersona { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string? TipoIdentificacion { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(25, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string? Identificacion { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(100, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string? RazonSocial { get; set; }

        [MaxLength(300, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(100, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        [EmailAddress(ErrorMessage = DomainConstants.MENSAJE_CAMPO_EMAIL_ADDRESS)]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(length: 10, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH + " [10]")]
        [MinLength(length: 10, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MIN_LENGTH + " [10]")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public int? IdSector { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string? Latitud { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string? Longitud { get; set; }

        public string? Sector { get; set; }

        public string? Ip { get; set; }
        public long Usuario { get; set; }

        public ClienteModel()
        {

        }

        public ClienteModel(Cliente cliente)
        {
            Id = Utilities.Crypto.CifrarId(cliente.IdCliente);
            TipoIdentificacion = cliente.TipoIdentificacion;
            Identificacion = cliente.Identificacion;
            RazonSocial = cliente.RazonSocial;
            CorreoElectronico = cliente.CorreoElectronico;
            Direccion = cliente.Direccion;
            Telefono = cliente.Telefono;
            TipoPersona = cliente.TipoPersona ?? TipoPersona.Natural;
            Latitud = Utilities.Utilidades.DoubleToString_FrontCO(cliente.Latitud, 6);
            Longitud = Utilities.Utilidades.DoubleToString_FrontCO(cliente.Longitud, 6);
            IdSector = cliente.IdSector;
            Sector = cliente?.IdSectorNavigation?.Nombre ?? "";
        }

    }


}
