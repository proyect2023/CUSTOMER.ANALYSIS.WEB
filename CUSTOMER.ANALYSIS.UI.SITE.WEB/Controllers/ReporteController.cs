using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants.VentanasSoporte.Reporte)]
    public class ReporteController : BaseController
    {
        private readonly IClienteRepository _clienteRepository;

        public ReporteController(IClienteRepository clienteRepository)
        {
            this._clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConsultarVentas(EstadoPlan estado, DateTime? fechaInicio, DateTime? fechaFin)
        {
            fechaFin = new DateTime(fechaFin.Value.Year, fechaFin.Value.Month, fechaFin.Value.Day, 23, 59, 59);
            return PartialView("_Detalle", _clienteRepository.GetClientePlan(fechaInicio.Value, fechaFin.Value, estado));
        }
    }
}
