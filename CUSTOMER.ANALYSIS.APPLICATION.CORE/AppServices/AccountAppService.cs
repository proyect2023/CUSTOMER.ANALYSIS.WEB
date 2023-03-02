using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.DomainServices;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.AppServices
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IMailDomainService _mailDomainService;
        private readonly IStorageService _storageService;
        private readonly IAccountRepository _accountRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;
        private readonly IPermisoRepository _permisoRepository;
        private readonly INotificacionRepository _notificacionRepository;

        public AccountAppService(
            IMailDomainService mailDomainService,
            IStorageService storageService,
            IAccountRepository accountRepository, 
            IUsuarioRepository usuarioRepository, 
            IRolRepository rolRepository, 
            IPermisoRepository permisoRepository,
            INotificacionRepository notificacionRepository
            )
        {
            this._mailDomainService = mailDomainService;
            this._storageService = storageService;
            this._accountRepository = accountRepository;
            this._usuarioRepository = usuarioRepository;
            this._rolRepository = rolRepository;
            this._permisoRepository = permisoRepository;
            this._notificacionRepository = notificacionRepository;
        }

        public MethodResponseDto CambiarPassword(long id, string ClaveActual, string ClaveNueva, string ClaveNuevaConfirma, bool EsCorreoRecuperacion)
        {
            MethodResponseDto responseDto = new MethodResponseDto();

            try
            {
                var error = "";
                var usuario = _accountRepository.GetFirstOrDefault(x => x.IdUsuario == id && (x.Estado == (EstadoUsuario.Activo) || x.Estado == (EstadoUsuario.Bloqueado)));
                if (usuario == null)
                {
                    //responseDto.CodigoError = DomainConstants.AGENTES_INACTIVOS;
                    responseDto.Mensaje = "Datos de usuario incorrecto";
                    return responseDto;
                }

                if (!Utilidades.ValidarClave(ClaveActual, usuario.Password, ClaveNueva, ClaveNuevaConfirma, EsCorreoRecuperacion, ref error))
                {
                    responseDto.Mensaje = error;
                    return responseDto;
                }

                //Valido las credenciales

                usuario.Password = GSCrypto.ComputeHashV1(ClaveNueva);
                usuario.FechaActualizarPassword = Utilities.Utilidades.GetHoraActual().AddDays(GlobalSettings.LoginAppDiasForzarCambioPassword);
                //usuario.FechaModificacion = Utilities.Utilidades.GetHoraActual();

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

        public (List<Menu>, string) LoadMenu(long IdUsuario)
        {
            try
            {
                string mensaje = "";
                var resultVentana = _accountRepository.ConsultarVentana(IdUsuario, ref mensaje);
                if (resultVentana == null) throw new Exception(mensaje);
                return (GenerarVentanaHabilitadas(resultVentana, null), null);
            }
            catch (Exception ex)
            {
                return (null, string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex)));
            }
        }

        public MethodResponseDto Login(string username, string password, string ip, bool fake = false)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                DateTime fechaActual = Utilities.Utilidades.GetHoraActual();
                string error = null;
                responseDto.Mensaje = AppConstants.MensajeUsuarioLogin;

                Usuario usuario = _accountRepository.GetFirstOrDefault(x => x.Username == username && (x.Estado == (EstadoUsuario.Activo) || x.Estado == (EstadoUsuario.Bloqueado)));
                if (usuario == null) return responseDto;

                if (usuario.Estado == (EstadoUsuario.Bloqueado))
                {
                    responseDto.Mensaje = AppConstants.MensajeUsuarioBloqueado;
                    return responseDto;
                }

                if (!fake && !GSCrypto.ConfirmHashV1(password, usuario.Password))
                {
                    if (usuario.IntentosFallidos is null) usuario.IntentosFallidos = 0;
                    usuario.IntentosFallidos++;

                    if (usuario.IntentosFallidos >= GlobalSettings.LoginAppNumeroIntentoBloqueo)
                    {
                        usuario.Estado = (EstadoUsuario.Bloqueado);

                        responseDto.Mensaje = AppConstants.MensajeUsuarioBloqueado;
                    }

                    _accountRepository.Update(usuario);
                    _accountRepository.Save();

                    return responseDto;
                }

                var resultVentana = _accountRepository.ConsultarVentana(usuario.IdUsuario, ref error);
                var roles = _rolRepository.GetRolesUsuario(usuario.IdUsuario);

                var correoAdmin = _usuarioRepository.GetUsuariosAdministrador().Select(c => c.CorreoElectronico);

                var login = new LoginAppResultDto
                {
                    IdUsuario = usuario.IdUsuario,
                    Usuario = usuario.Username,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Correo = usuario.CorreoElectronico,
                    CorreosAdministrador = correoAdmin.ToArray().ConcatenarValores(),
                    ForzarCambioClave = false,
                    IPLogin = ip,
                    FechaUltimaConexion = usuario.FechaUltimaConexion,
                    Menu = GenerarVentanaHabilitadas(resultVentana, null),
                    VentanasActivasConcat = GenerarVentanaHabilitadasConcat(resultVentana),
                    Rol = roles?.FirstOrDefault()?.IdRol ?? 0,
                    Foto = usuario.Foto,
                    //TimeZoneId
                };

                //primera vez
                if (usuario.FechaUltimaConexion is null) login.ForzarCambioClave = true;

                if (usuario.FechaActualizarPassword is null) usuario.FechaActualizarPassword = fechaActual.AddDays(GlobalSettings.LoginAppDiasForzarCambioPassword);
                if (usuario.FechaActualizarPassword <= fechaActual) login.ForzarCambioClave = true;

                usuario.FechaUltimaConexion = fechaActual;
                usuario.IntentosFallidos = 0;

                var acceso = new AccesoUsuario
                {
                    Fecha = usuario.FechaUltimaConexion ?? fechaActual,
                    IdUsuario = usuario.IdUsuario,
                    Ip = ip,
                    SitioWeb = DomainConstants.COMPONENTE_NAME
                };

                _accountRepository.RegistrarAccesoUsuario(acceso);

                _accountRepository.Update(usuario);
                _accountRepository.Save();

                login.CantidadNotificaciones = _notificacionRepository.GetNotificaciones(usuario.IdUsuario, false).Count();

                responseDto.Data = login;
                responseDto.Estado = true; // _accountRepository.RegistrarAccesoUsuario(acceso);
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto RecuperarPassword(string username, string email)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                string Password = APPLICATION.CORE.Utilities.Utilidades.RandomString();

                var usuario = _accountRepository.GetFirstOrDefault(x => x.Username == username && x.CorreoElectronico == email && (x.Estado == (EstadoUsuario.Activo) || x.Estado == (EstadoUsuario.Bloqueado)));
                if (usuario == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_USUARIO_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                usuario.Estado = EstadoUsuario.Activo;
                usuario.Password = GSCrypto.ComputeHashV1(Password);
                usuario.FechaUltimaConexion = null;

                _accountRepository.Update(usuario);
                responseDto.Estado = _accountRepository.Save() > 0;

                _mailDomainService.EnviarMailRecuperarPassword(
                    usuario.CorreoElectronico,
                    usuario.Nombre + " " + usuario.Apellido,
                    Password
                    );
                //_envioMailService.EnviarMail(mail);
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        private List<Menu> GenerarVentanaHabilitadas(List<VentanaLoginDto> ventanasMaestra, long? IdPadre)
        {
            List<Menu> menu = new List<Menu>();
            if (ventanasMaestra != null && ventanasMaestra.Any())
            {
                var VentanaPadre = ventanasMaestra.Where(x => x.IdPadre == IdPadre).ToList();
                foreach (var i in VentanaPadre)
                {
                    Menu ventana = new Menu
                    {
                        Icon = i.Icono,
                        Label = i.NombreAbreviado,
                        Url = i.Url,
                        IdRol = i.IdRol,
                        Rol = i.Rol,
                        Order = i.Orden,
                        SubMenu = GenerarVentanaHabilitadas(ventanasMaestra, i.IdPermiso)
                    };

                    menu.Add(ventana);
                }
            }

            return menu;
        }

        private string GenerarVentanaHabilitadasConcat(List<VentanaLoginDto> ventanasDto)
        {
            StringBuilder sb = new StringBuilder();

            if (ventanasDto != null && ventanasDto.Count > 0)
            {
                foreach (var i in ventanasDto)
                    sb.Append($"{i.Codigo};");
            }

            return sb.ToString();
        }
    }
}
