using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants.VentanasSoporte.Clientes)]
    public class ClienteController : Controller
    {
        private readonly IAnalisisQueryService _analisisQueryService;

        public ClienteController(IAnalisisQueryService analisisQueryService)
        {
            this._analisisQueryService = analisisQueryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get(bool masVendidos = false, bool antiguos = false, int estadoClientePlan = 0)
        {
            var result = _analisisQueryService.ConsultarTotales(masVendidos, antiguos, estadoClientePlan);

            return Json(result);
        }
    }
}
