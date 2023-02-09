using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using System;
using GS.TOOLS;
using System.Linq;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.DomainServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.Services;
using System.Collections.Generic;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Models;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.AppServices
{
    public sealed class UsuarioAppService : BaseAppService, IUsuarioAppService
    {
        private readonly IMailDomainService _envioMailService;
        private readonly IUsuarioRepository _usuarioRepository;
        //private readonly IUsuarioQueryService _usuarioQueryService;
        private readonly IStorageService _storageService;

        public UsuarioAppService(
            IStorageService storageService,
            IMailDomainService envioMailService, 
            IUsuarioRepository usuarioRepository
            //IUsuarioQueryService usuarioQueryService
            )
        {
            _storageService = storageService;
            this._envioMailService = envioMailService;
            this._usuarioRepository = usuarioRepository;
            //this._usuarioQueryService = usuarioQueryService;
        }

        public MethodResponseDto CrearUsuario(UsuariosAppResultDto usuariosApp, long IdUsuarioCreacion, string Ip)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                Usuario usuarioExiste = _usuarioRepository.GetFirstOrDefault(x => x.Username == usuariosApp.Usuario && (x.Estado == (EstadoUsuario.Activo) || x.Estado == (EstadoUsuario.Bloqueado)));
                if (usuarioExiste != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_USUARIO_REGISTRADO_USERNAME;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                usuarioExiste = _usuarioRepository.GetFirstOrDefault(x => x.CorreoElectronico == usuariosApp.CorreoElectronico && (x.Estado == (EstadoUsuario.Activo) || x.Estado == (EstadoUsuario.Bloqueado)));
                if (usuarioExiste != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_USUARIO_REGISTRADO_MAIL;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }
                
                usuarioExiste = _usuarioRepository.GetFirstOrDefault(x => x.Cedula == usuariosApp.Cedula && (x.Estado == (EstadoUsuario.Activo) || x.Estado == (EstadoUsuario.Bloqueado)));
                if (usuarioExiste != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_USUARIO_REGISTRADO_CEDULA;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                string Password = APPLICATION.CORE.Utilities.Utilidades.RandomString();

                Usuario usuario = new Usuario
                {
                    Nombre = usuariosApp.Nombre,
                    Apellido = usuariosApp.Apellido,
                    Cedula = usuariosApp.Cedula,
                    Direccion = usuariosApp.Direccion,
                    FechaIncorporacion = usuariosApp.FechaIncorporacion,

                    Username = usuariosApp.Usuario,
                    Password = GSCrypto.ComputeHashV1(Password),

                    CorreoElectronico = usuariosApp.CorreoElectronico,
                    Telefono = usuariosApp.Telefono,
                    FechaActualizarPassword = Utilidades.GetHoraActual().AddDays(GlobalSettings.LoginAppDiasForzarCambioPassword),
                    
                    Ip = Ip,
                    FechaCreacion = Utilidades.GetHoraActual(),
                    UsuarioCreacion = IdUsuarioCreacion,
                    Estado = (EstadoUsuario.Activo),
                };

                _usuarioRepository.Add(usuario);
                responseDto.Estado = _usuarioRepository.Save() > 0;

                if (responseDto.Estado)
                {
                    _envioMailService.EnviarMailBienvenida(
                        usuario.CorreoElectronico,
                        usuario.Nombre + " " + usuario.Apellido,
                        usuario.Username,
                        Password
                        );

                    //_envioMailService.EnviarMail(new MailDto
                    //{
                    //    Asunto = usuario.Username,
                    //    Mensaje = Password,
                    //    Correos = usuario.CorreoElectronico,
                    //    FechaIngreso = Utilidades.GetHoraActual(),
                    //    Tipo = TipoMail.Bienvenida,
                    //    Copias = "",
                    //    CopiasOcultas = "",

                    //    //Servicio = Constants.Servicio.
                    //});
                }

                UsuarioRol rol = new UsuarioRol
                {
                    IdRol = usuariosApp.IdRol,
                    IdUsuario = usuario.IdUsuario,
                    Estado = true
                };

                _usuarioRepository.GuardarUsuarioRol(rol);

            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto EditarUsuario(UsuariosAppResultDto usuariosApp, long IdUsuarioCreacion, string Ip)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var usuario = _usuarioRepository.Get(long.Parse(Crypto.DescifrarId(usuariosApp.IdUsuario)));
                if (usuario == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_USUARIO_NO_REGISTRADO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                Usuario usuarioExiste = null;
                if (usuario.Username != usuariosApp.Usuario)
                {
                    usuarioExiste = _usuarioRepository.GetFirstOrDefault(x => x.Username == usuariosApp.Usuario && (x.Estado == (EstadoUsuario.Activo) || x.Estado == (EstadoUsuario.Bloqueado)));
                    if (usuarioExiste != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_USUARIO_REGISTRADO_USERNAME;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                if (usuario.CorreoElectronico != usuariosApp.CorreoElectronico)
                {
                    usuarioExiste = _usuarioRepository.GetFirstOrDefault(x => x.CorreoElectronico == usuariosApp.CorreoElectronico && (x.Estado == (EstadoUsuario.Activo) || x.Estado == (EstadoUsuario.Bloqueado)));
                    if (usuarioExiste != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_USUARIO_REGISTRADO_MAIL;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }
                
                if (usuario.Cedula != usuariosApp.Cedula)
                {
                    usuarioExiste = _usuarioRepository.GetFirstOrDefault(x => x.Cedula == usuariosApp.Cedula && (x.Estado == (EstadoUsuario.Activo) || x.Estado == (EstadoUsuario.Bloqueado)));
                    if (usuarioExiste != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_USUARIO_REGISTRADO_CEDULA;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                usuario.Nombre = usuariosApp.Nombre;
                usuario.Apellido = usuariosApp.Apellido;
                usuario.Direccion = usuariosApp.Direccion;
                usuario.Cedula = usuariosApp.Cedula;
                usuario.FechaIncorporacion = usuariosApp.FechaIncorporacion;
                
                usuario.Username = usuariosApp.Usuario;

                usuario.CorreoElectronico = usuariosApp.CorreoElectronico;
                usuario.Telefono = usuariosApp.Telefono;

                usuario.Ip = Ip;
                usuario.UsuarioModificacion = IdUsuarioCreacion;
                usuario.FechaModificacion = Utilidades.GetHoraActual();

                _usuarioRepository.Update(usuario);
                responseDto.Estado = _usuarioRepository.Save() > 0;

                UsuarioRol rol = new UsuarioRol
                {
                    IdRol = usuariosApp.IdRol,
                    IdUsuario = usuario.IdUsuario,
                    Estado = true
                };

                _usuarioRepository.GuardarUsuarioRol(rol);
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto ConsultarUsuarios()
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _usuarioRepository.ConsultarUsuarios();
                if (result != null)
                {
                    responseDto.Estado = true;
                    responseDto.Data = result.Select(c => new UsuariosAppResultDto(c, c.UsuarioRol.ToList())).ToList();
                }
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public (bool, string) EliminarUsuario(string IdCifrado, long IdUsuario, string Ip)
        {
            try
            {
                long Id = long.Parse(IdCifrado);
                var result = _usuarioRepository.GetFirstOrDefault(x =>x.IdUsuario==Id && (x.Estado == EstadoUsuario.Activo || x.Estado == EstadoUsuario.Bloqueado)) ;
                if (result == null)
                {
                    return (false, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_USUARIO_NO_REGISTRADO));
                }

                result.FechaEliminacion = CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities.Utilidades.GetHoraActual();
                result.UsuarioEliminacion = IdUsuario;
                result.Ip = Ip;
                result.Estado = 0;
                _usuarioRepository.Update(result);

                //return true;
                return (_usuarioRepository.Save() > 0, null);
            }
            catch (Exception ex)
            {
                //return false;
                return (false, string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex)));
            }
        }

        public MethodResponseDto ObtenerUsuario(long idUsuario)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _usuarioRepository.ObtenerUsuario(idUsuario);
                if (result != null)
                {
                    responseDto.Data = new UsuariosAppResultDto(result, result.UsuarioRol.ToList());
                    responseDto.Estado = true;
                }

            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto ConsultarPerfil(long Id)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _usuarioRepository.ObtenerUsuario(Id);
                if (result != null)
                {
                    responseDto.Estado = true;
                    var model = new PerfilModel(result, result.UsuarioRol.ToList());
                    string imagen = _storageService.ObtenerImagenBase64(GlobalSettings.TipoAlmacenamiento, model.ImagenRuta ?? "", "");
                    model.ImagenBase64 = string.IsNullOrEmpty(imagen) ? AppConstants.SinImagen : imagen;

                    responseDto.Data = model;
                }
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto ActualizarPerfil(PerfilModel model, long IdUsuarioCreacion, string Ip)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var usuario = _usuarioRepository.Get(IdUsuarioCreacion);
                if (usuario == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_USUARIO_NO_REGISTRADO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                model.Usuario = usuario.Username;
                if (string.IsNullOrEmpty(usuario.Foto))
                {
                    model.ImagenBase64 = AppConstants.SinImagen;
                }
                else
                {
                    string imagen = _storageService.ObtenerImagenBase64(GlobalSettings.TipoAlmacenamiento, usuario.Foto ?? "", $"foto-perfil-{usuario.Username}");
                    model.ImagenBase64 = string.IsNullOrEmpty(imagen) ? AppConstants.SinImagen : imagen;
                }

                Usuario usuarioExiste = null;
                if (usuario.CorreoElectronico != model.CorreoElectronico)
                {
                    usuarioExiste = _usuarioRepository.GetFirstOrDefault(x => x.CorreoElectronico == model.CorreoElectronico && (x.Estado == (EstadoUsuario.Activo) || x.Estado == (EstadoUsuario.Bloqueado)));
                    if (usuarioExiste != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_USUARIO_REGISTRADO_MAIL;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                if (model.Imagen != null && model.Imagen.Any())
                {
                    List<ArchivoServiceDto> imagenes = _storageService.GuardarImagenes(model.Imagen, "Perfil", usuario.Username);
                    usuario.Foto = imagenes.FirstOrDefault()?.Ruta ?? "";
                    string imagen = _storageService.ObtenerImagenBase64(GlobalSettings.TipoAlmacenamiento, usuario.Foto ?? "", "");
                    model.ImagenBase64 = string.IsNullOrEmpty(imagen) ? AppConstants.SinImagen : imagen;
                }

                usuario.Nombre = model.Nombre;
                usuario.Apellido = model.Apellido;
                usuario.CorreoElectronico = model.CorreoElectronico;
                usuario.Telefono = model.Telefono;
                usuario.Direccion = model.Direccion;

                usuario.Ip = Ip;
                usuario.UsuarioModificacion = IdUsuarioCreacion;
                usuario.FechaModificacion = Utilidades.GetHoraActual();

                usuario.Ip = Ip;
                usuario.UsuarioModificacion = IdUsuarioCreacion;
                usuario.FechaModificacion = Utilidades.GetHoraActual();

                _usuarioRepository.Update(usuario);
                responseDto.Estado = _usuarioRepository.Save() > 0;

            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }
    }
}
