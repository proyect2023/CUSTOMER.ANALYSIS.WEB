using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants.VentanasSoporte.Mapa)]
    public class MapaController : BaseController
    {
        private readonly IUtilidadRepository _utilidadRepository;
        private readonly IAnalisisQueryService _analisisQueryService;

        public MapaController(IUtilidadRepository utilidadRepository, IAnalisisQueryService analisisQueryService)
        {
            this._utilidadRepository = utilidadRepository;
            this._analisisQueryService = analisisQueryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.sectores = _utilidadRepository.GetSectores();
            return View();
        }
        
        [HttpGet]
        public IActionResult Calor()
        {
            ViewBag.sectores = _utilidadRepository.GetSectores();
            return View();
        }

        [HttpGet]
        public JsonResult Get(bool masVendidos = false, bool antiguos = false, int estadoClientePlan = 0, int sector = 0)
        {
            var result = _analisisQueryService.ConsultarTotales(masVendidos, antiguos, estadoClientePlan, sector);

            return Json(result);
        }
    }
}
