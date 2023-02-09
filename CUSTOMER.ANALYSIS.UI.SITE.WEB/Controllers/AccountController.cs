using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GS.TOOLS;
using static System.Net.Mime.MediaTypeNames;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Parameters;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Cryptography;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Models;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Parameters;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    //[Microsoft.AspNetCore.Authorization.Authorize]
    //[Filters.MenuFilter(Constants.VentanasSoporte.Proveedores)]
    public class AccountController : BaseController
    {
        private readonly EFContext _eFContext;
        private readonly IConfiguration _configuration;
        private readonly IAccountAppService _accountAppService;
        private readonly IStorageService _storageService;

        public AccountController(
            EFContext eFContext,
            IConfiguration configuration,
            //ILogActividadService logActividadService,
            IAccountAppService accountAppService, 
            IStorageService storageService,
            ILogInfraServices logInfraServices
            ) 
            : base(logInfraServices)
        {
            this._eFContext = eFContext;
            this._configuration = configuration;
            this._accountAppService = accountAppService;
            this._storageService = storageService;
        }


        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsuarioLoginPageViewModel usr, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        var google = new GSReCaptchaGoogleDto
                        {
                            ReCaptchaClaveSitioWeb = WebSiteParameters.WebReCaptchaClaveSitioWeb,
                            ReCaptchaClaveComGoogle = WebSiteParameters.WebReCaptchaClaveComGoogle,
                            ValorReCaptcha = null,
                            ReCaptchaCont = 0,
                            EncodedResponse = usr.Captcha
                        };

                        string mensaje = null;
                        var resultGoogle = GSRecaptchaGoogle.Validar(ref google, ref mensaje);

                        if (!resultGoogle)
                        {
                            RegistrarLogError(this.GetCaller(), mensaje);
                            throw new Exception(AppParameters.ExcepcionGenerica);
                        }

                        var IPLogin = HttpContext.Connection.RemoteIpAddress.ToString();

                        var resultLogin = _accountAppService.Login(usr.Usuario, usr.Clave, IPLogin);
                        if (resultLogin.TieneErrores) throw new Exception(resultLogin.MensajeError);

                        if (!resultLogin.Estado)
                        {
                            ModelState.AddModelError("", resultLogin.Mensaje);
                            return View(usr);
                        }

                        LoginAppResultDto loginAppResult = resultLogin.Data;

                        string imagen = _storageService.ObtenerImagenBase64(GlobalSettings.TipoAlmacenamiento, loginAppResult.Foto ?? "", "");

                        HttpContext.Session.SetString("FotoBase64", string.IsNullOrEmpty(imagen) ? AppConstants.SinImagen : imagen);
                        HttpContext.Session.SetString("Menu", JsonConvert.SerializeObject(loginAppResult.Menu));
                        HttpContext.Session.SetInt32("CantidadNotificaciones", loginAppResult.CantidadNotificaciones);

                        loginAppResult.Menu = null;

                        string RolStr = "";
                        if (loginAppResult.Rol > 4 && loginAppResult.Rol < 1)
                        {
                            RolStr = Roles.Estandar.ToString();
                        }
                        else
                        {
                            RolStr = ((Roles)loginAppResult.Rol).ToString();
                        }

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, loginAppResult.Nombre + " " + loginAppResult.Apellido),
                            new Claim(ClaimTypes.Role, RolStr),
                            new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(loginAppResult))
                        };

                        await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));

                        if (loginAppResult.ForzarCambioClave)
                            return RedirectToAction("ChangePassword");
                        else
                        {
                            if (WebSiteParameters.WebLimiteConsulta == "")
                            {
                                WebSiteParameters.WebLimiteConsulta = "3[MM]";
                            }
                            WebSiteParameters.WebLimiteConsulta = WebSiteParameters.WebLimiteConsulta.ToUpper();
                        }
                    }

                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Dashboard");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(usr);
        }

        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Menu");
            HttpContext.Session.Remove("CantidadNotificaciones");
            HttpContext.Session.Clear();
            GS.TOOLS.GSUtilities.ClearMemory();
            return RedirectToAction("Login");
        }


        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UsuarioCambioClaveViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userLogin = GetUserLogin();
                    var result = _accountAppService.CambiarPassword(userLogin.IdUsuario, model.ClaveActualConfirma, model.ClaveNueva, model.ClaveNuevaConfirma, true);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);

                    if (!result.Estado)
                    {
                        ModelState.AddModelError("", result.Mensaje);
                    }
                    else
                    {
                        TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", $"Se ha cambiado la contraseña con exito. Vuelva a iniciar sesión");

                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        return RedirectToAction("Login");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult RecuperarClave()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecuperarClave(UsuarioRecuperaClaveViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _accountAppService.RecuperarPassword(user.Usuario, user.CorreoElectronico);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);
                    if (result.Estado)
                    {
                        TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", $"Se ha enviado un correo electrónico al correo {APPLICATION.CORE.Utilities.Utilidades.OcultarCaracteres(user.CorreoElectronico, user.CorreoElectronico.Split("@")[0].Length)} registrado para poder recuperar su contraseña. Gracias");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", result.Mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(user);
        }

        public IActionResult ReloadSesion()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                //recargar sesion
            }

            return RedirectToAction("Index", "Dashboard");
        }



    }
}
