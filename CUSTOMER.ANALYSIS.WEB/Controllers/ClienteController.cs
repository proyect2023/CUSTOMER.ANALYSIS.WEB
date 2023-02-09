using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CUSTOMER.ANALYSIS.WEB.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            this._clienteRepository = clienteRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get()
        {
            var cliente = _clienteRepository.GetAll();

            return Json(cliente);
        }
    }
}
