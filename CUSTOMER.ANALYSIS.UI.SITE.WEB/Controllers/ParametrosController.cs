using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.DomainServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.DomainServices;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        //public IActionResult Guardar(ParametroDto parametro)
        //{

        //}

    }
}
