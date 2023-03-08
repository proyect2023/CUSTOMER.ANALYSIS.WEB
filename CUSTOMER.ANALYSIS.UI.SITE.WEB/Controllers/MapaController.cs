using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants.VentanasSoporte.Mapa)]
    public class MapaController : BaseController
    {
        private readonly IPlanRepository _planRepository;
        private readonly IUtilidadRepository _utilidadRepository;
        private readonly IAnalisisQueryService _analisisQueryService;

        public MapaController(
            IPlanRepository planRepository,
            IUtilidadRepository utilidadRepository, IAnalisisQueryService analisisQueryService)
        {
            this._planRepository = planRepository;
            this._utilidadRepository = utilidadRepository;
            this._analisisQueryService = analisisQueryService;
        }

        [Filters.MenuFilter(CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants.VentanasSoporte.MapaMarcador)]
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["sectores"] = new SelectList(_utilidadRepository.GetSectores().ToList(), "IdSector", "Nombre");
            ViewData["planes"] = new SelectList(_planRepository.GetPlanes().ToList(), "IdPlan", "Nombre");

            return View();
        }

        [Filters.MenuFilter(CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants.VentanasSoporte.MapaCalor)]
        [HttpGet]
        public IActionResult Calor()
        {
            ViewData["sectores"] = new SelectList(_utilidadRepository.GetSectores().ToList(), "IdSector", "Nombre");
            ViewData["planes"] = new SelectList(_planRepository.GetPlanes().ToList(), "IdPlan", "Nombre");

            return View();
        }

        [HttpPost]
        public JsonResult Get(string[] validaciones, string[] sectores, string[] planes)
        {
            var result = _analisisQueryService.ConsultarTotales(validaciones, sectores, planes);

            return Json(result);
        }
    }
}
