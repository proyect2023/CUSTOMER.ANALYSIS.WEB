using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.DomainServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.DomainServices;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    [Authorize]
    public class ParametrosController : BaseController
    {
        private readonly IAnalisisDomainService _analisisDomainService;

        public ParametrosController(IAnalisisDomainService analisisDomainService)
        {
            this._analisisDomainService = analisisDomainService;
        }

        public IActionResult Index()
        {
            List<ParametroDto> list = new List<ParametroDto>();
            try
            {
                var result = _analisisDomainService.ConsultarParametros();
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    list = result.Data;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }
            
            return View(list);
        }

        public IActionResult Guardar(ParametroDto parametro)
        {
            try
            {
                if (parametro == null) return Json(new ResponseToViewDto { Estado = false, Mensaje = "" });
                if (string.IsNullOrEmpty(parametro.Codigo)) return Json(new ResponseToViewDto { Estado = false, Mensaje = "Ingrese un valor en el campo Código" });
                if (string.IsNullOrEmpty(parametro.Valor)) return Json(new ResponseToViewDto { Estado = false, Mensaje = "Ingrese un valor en el campo Valor" });

                var usuarioSesion = GetUserLogin();

                parametro.Usuario = usuarioSesion.Usuario;
                parametro.Ip = usuarioSesion.IPLogin;

                var result = _analisisDomainService.GuardarParametro(parametro);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                return Json(new ResponseToViewDto { Estado = result.Estado, Mensaje = result.Mensaje });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = ex.Message });
            }
        }

    }
}
