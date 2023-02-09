
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Parameters;
using CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Models;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    [Authorize]
    [Filters.MenuFilter(VentanasSoporte.Rol)]
    public class RolController : BaseController
    {
        private readonly IRolAppService _rolAppService;

        public RolController(
            IRolAppService rolAppService, 
            ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            this._rolAppService = rolAppService;
        }

        public IActionResult Index()
        {
            var userLogin = GetUserLogin();
            (var roles, string error) = _rolAppService.ConsultarRoles(userLogin.IdUsuario);

            if(roles == null || !roles.Any())
                TempData["msg"] = "<script>$.jGrowl('No existen roles para mostrar', { life: 5000, theme: 'growl-success' });</script>";
            
            if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);

            //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_CARGO);

            return View(roles);
        }

        [HttpPost]
        public IActionResult Guardar(RolViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userLogin = GetUserLogin();

                if (model.IdRol == 0)
                {
                    (var result, string error) = _rolAppService.RegistrarRol(userLogin.IdUsuario, model.Nombre, model.Estado == 1);
                    if (result) TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Rol registrado con éxito");
                    else TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_ERROR.Replace("{Mensaje_Respuesta}", "La información ingresada es inválida");
                    if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);
                    //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_CARGO_CREAR);
                }
                else
                {
                    (var result, string error) = _rolAppService.ActualizarRol(model.IdRol, model.Nombre, model.Estado == 1, userLogin.IdUsuario);
                    if (result) TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Rol actualizado con éxito");
                    else TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_ERROR.Replace("{Mensaje_Respuesta}", "La información ingresada es inválida");
                    if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);
                    //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_CARGO_EDITAR, $"Id registro = {model.IdRol}");
                }
            }
            else
                TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_ERROR.Replace("{Mensaje_Respuesta}", "La información ingresada es inválida");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ConsultarVentanasActivas()
        {
            try
            {
                var userLogin = GetUserLogin();
                (var result, string error) = _rolAppService.ConsultarVentanasActivas(userLogin.Usuario);
                if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);
                return Json(new
                {
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, AppParameters.ExcepcionGenerica + RegistrarLogError(this.GetCaller(), ex));
            }
        }

        [HttpPost]
        public IActionResult ConsultarRolVentanas(short IdRol)
        {
            try
            {
                var userLogin = GetUserLogin();

                (var result, string error) = _rolAppService.ConsultarRolVentanas(IdRol, userLogin.Usuario);
                if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);
                if (result == null) return StatusCode(StatusCodes.Status500InternalServerError, AppParameters.ExcepcionGenerica);
                return Json(new
                {
                    data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, AppParameters.ExcepcionGenerica + RegistrarLogError(this.GetCaller(), ex));
            }
        }

        [HttpPost]
        public IActionResult AsignaRolVentanas(short IdRol, string Nombre, string IdVentanas)
        {
            try
            {
                var userLogin = GetUserLogin();

                (var result, string error) = _rolAppService.Asignar(IdRol, IdVentanas, userLogin.IdUsuario);
                if (!string.IsNullOrEmpty(error)) RegistrarLogError(this.GetCaller(), error);
                if (result == null) return StatusCode(StatusCodes.Status500InternalServerError, AppParameters.ExcepcionGenerica);
                if (result == 0)
                {
                    var mensaje = $"Rol [{Nombre}] no encontrado.";
                    return StatusCode(StatusCodes.Status500InternalServerError, mensaje);
                }
                //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_PERMISOS_ASIGNAR);
                return StatusCode(StatusCodes.Status200OK, "Registro actualizado correctamente");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, AppParameters.ExcepcionGenerica + RegistrarLogError(this.GetCaller(), ex));
            }
        }
    }
}
