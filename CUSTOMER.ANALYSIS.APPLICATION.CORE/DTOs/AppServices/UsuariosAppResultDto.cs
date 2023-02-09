
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public class UsuariosAppResultDto
    {
        public string? IdUsuario { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(20, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH)]
        public string? Cedula { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string? Usuario { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [EmailAddress(ErrorMessage = DomainConstants.MENSAJE_CAMPO_EMAIL_ADDRESS)]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(length: 10, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MAX_LENGTH + " [10]")]
        [MinLength(length: 10, ErrorMessage = DomainConstants.MENSAJE_CAMPO_MIN_LENGTH + " [10]")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public long IdRol { get; set; }
        public string? NombreRol { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        [MaxLength(200)]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public DateTime FechaIncorporacion { get; set; }

        public UsuariosAppResultDto()
        {

        }


        
        public UsuariosAppResultDto(Usuario usuario, List<UsuarioRol> usuarioRol)
        {
            Rol rol = null;

            if (usuarioRol != null && usuarioRol.Any())
            {
                rol = usuarioRol.FirstOrDefault().IdRolNavigation;
            }

            IdRol = rol?.IdRol ?? 0;
            NombreRol = rol?.Nombre ?? "";

            IdUsuario = Crypto.CifrarId(usuario.IdUsuario);
            Nombre = usuario.Nombre;
            Usuario = usuario.Username;
            CorreoElectronico = usuario.CorreoElectronico;
            Apellido = usuario.Apellido;
            Telefono = usuario.Telefono;
            Direccion = usuario.Direccion;
            Cedula = usuario.Cedula;


            FechaIncorporacion = usuario.FechaIncorporacion ??new DateTime(2000, 01, 01);
            
            //ImagenBase64 = AppConstants.SinImagen;
        }
    
    }
}
