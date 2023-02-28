using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Models;
using CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Luilliarcec.Identification.Ecuador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    public class PlanesController : BaseController
    {
        private readonly IPlanAppService _planAppService;
        private readonly IUtilidadRepository _utilidadRepository;

        public PlanesController(
            IPlanAppService planAppService,
            IUtilidadRepository utilidadRepository,
            ILogInfraServices logInfraServices,
            IClienteAppService clienteAppService)
        {
            _planAppService = planAppService;
            _utilidadRepository = utilidadRepository;
        }

        public IActionResult Index()
        {
            List<PlanViewModel> clientes = new List<PlanViewModel>();
            try
            {
                
                var result = _planAppService.ConsultarPlanes();
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    clientes = result.Data;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }
            finally
            {
                //RegistrarActividad(CodigoActividad.COD_INFORMACION_GENERAL_CLIENTE);

            }

            return View(clientes);
        }

        [HttpGet]
        public IActionResult Registrar(string Id)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(Id);

            PlanViewModel model = new PlanViewModel();
            try
            {
                ViewData["tipoPlan"] = new SelectList(_utilidadRepository.GetTiposPlan().ToList(), "IdTipoPlan", "Nombre");
                if (!string.IsNullOrEmpty(Id))
                {
                    var result = _planAppService.ConsultarPlan(Id);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);
                    if (result.Estado)
                    {
                        //RegistrarActividad(CodigoActividad.COD_INFORMACION_GENERAL_CLIENTE_VER, $"Id registro = [{Crypto.DescifrarId(Id)}]");
                        model = result.Data;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Mensaje);
                    }
                }
            }
            catch (System.Exception ex)
            {
                model = new PlanViewModel();
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }
            finally
            {
                //RegistrarActividad(CodigoActividad.COD_INFORMACION_GENERAL_CLIENTE_VER, $"Id registro = [{Crypto.DescifrarId(Id)}]");

            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Registrar(PlanViewModel model)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(model.Id);
            try
            {
                ViewData["tipoPlan"] = new SelectList(_utilidadRepository.GetTiposPlan().ToList(), "IdTipoPlan", "Nombre");
                if (ModelState.IsValid)
                {
                    var usr = GetUserLogin();
                    model.Ip = usr.IPLogin;
                    model.Usuario = usr.IdUsuario;

                    if (string.IsNullOrEmpty(model.Id))
                    {
                        var result = _planAppService.CrearPlan(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            //RegistrarActividad(CodigoActividad.COD_INFORMACION_GENERAL_CLIENTE_CREAR);
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Cliente registrado con éxito");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                    else
                    {
                        var result = _planAppService.EditarPlan(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            //RegistrarActividad(CodigoActividad.COD_INFORMACION_GENERAL_CLIENTE_EDITAR, $"Id registro = [{Crypto.DescifrarId(model.Id)}]");
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Cliente actualizado con éxito");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Eliminar(string Id)
        {
            try
            {
                var usr = GetUserLogin();
                var result = _planAppService.EliminarPlan(Id, usr.IPLogin, usr.IdUsuario);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    //RegistrarActividad(CodigoActividad.COD_INFORMACION_GENERAL_CLIENTE_ELIMINAR, $"Id registro = [{Crypto.DescifrarId(Id)}]");
                    return Json(new ResponseToViewDto { Estado = true, Mensaje = "Cliente eliminado con éxito" });
                }
                else
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = "Error al eliminar el Cliente" });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

    }
}
