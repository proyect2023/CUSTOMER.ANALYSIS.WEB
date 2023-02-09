using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Models;
using CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CUSTOMER.ANALYSIS.WEB.Controllers
{
    [Authorize]
    public class PerfilController : BaseController
    {
        private readonly IUsuarioAppService _usuarioAppService;

        public PerfilController(
            IUsuarioAppService usuarioAppService,
            ILogInfraServices logInfraServices
            ) : base(logInfraServices)
        {
            _usuarioAppService = usuarioAppService;
        }

        public IActionResult Index()
        {
            PerfilModel model = new PerfilModel();
            try
            {
                var usr = GetUserLogin();

                var result = _usuarioAppService.ConsultarPerfil(usr.IdUsuario);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    model = result.Data;
                }
                else
                {
                    TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", result.Mensaje);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }
            finally
            {
                //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_PERFIL);
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(PerfilModel model)
        {
            try
            {
                var usr = GetUserLogin();

                var result = _usuarioAppService.ActualizarPerfil(model, usr.IdUsuario, usr.IPLogin);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_PERFIL_EDITAR);
                    HttpContext.Session.SetString("FotoBase64", model.ImagenBase64);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Mensaje);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }
            return View(model);
        }

        //[HttpGet]
        //public IActionResult GetActividades()
        //{
        //    try
        //    {
        //        var result = _actividadDomainService.ConsultarActividadManejos();
        //        if (result.TieneErrores) throw new Exception(result.MensajeError);
        //        if (result.Estado)
        //        {
        //            return Json(new ResponseToViewDto { Estado = true, Data = result.Data });
        //        }
        //        else
        //        {
        //            return Json(new ResponseToViewDto { Estado = false, Mensaje = (string.IsNullOrEmpty(result.Mensaje) ? "Error al consultar los registros" : result.Mensaje) });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
        //    }
        //}
    }
}
