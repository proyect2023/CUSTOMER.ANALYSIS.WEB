using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Models;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Luilliarcec.Identification.Ecuador;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants.VentanasSoporte.Clientes)]
    public class ClienteController : BaseController
    {
        private readonly IAnalisisQueryService _analisisQueryService;
        private readonly IUtilidadRepository _utilidadRepository;
        private readonly IClienteAppService _clienteAppService;

        public ClienteController(
            IAnalisisQueryService analisisQueryService,
            IUtilidadRepository utilidadRepository,
            ILogInfraServices logInfraServices,
            IClienteAppService clienteAppService) 
        {
            this._analisisQueryService = analisisQueryService;
            _utilidadRepository = utilidadRepository;
            _clienteAppService = clienteAppService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registrar(string Id)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(Id);

            ClienteModel model = new ClienteModel();
            try
            {
                ViewData["tipoIdentificacion"] = new SelectList(_utilidadRepository.GetTipoIdentificaciones().ToList(), "Codigo", "Nombre");
                ViewData["sectores"] = new SelectList(_utilidadRepository.GetSectores().ToList(), "IdSector", "Nombre");

                if (!string.IsNullOrEmpty(Id))
                {
                    var result = _clienteAppService.ConsultarCliente(Id);
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
                model = new ClienteModel();
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }
            finally
            {
                //RegistrarActividad(CodigoActividad.COD_INFORMACION_GENERAL_CLIENTE_VER, $"Id registro = [{Crypto.DescifrarId(Id)}]");

            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Registrar(ClienteModel model)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(model.Id);
            try
            {
                ViewData["tipoIdentificacion"] = new SelectList(_utilidadRepository.GetTipoIdentificaciones(), "Codigo", "Nombre");
                ViewData["sectores"] = new SelectList(_utilidadRepository.GetSectores().ToList(), "IdSector", "Nombre");

                if (Identification.ValidateAllTypeIdentification(model.Identificacion) != model.TipoIdentificacion)
                {
                    ModelState.AddModelError("Identificacion", "La identificación ingresada no corresponde al tipo de identificación seleccionado");
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    var usr = GetUserLogin();
                    model.Ip = usr.IPLogin;
                    model.Usuario = usr.IdUsuario;

                    if (string.IsNullOrEmpty(model.Id))
                    {
                        var result = _clienteAppService.CrearCliente(model);
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
                        var result = _clienteAppService.EditarCliente(model);
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
                var result = _clienteAppService.EliminarCliente(Id, usr.IPLogin, usr.IdUsuario);
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

        [HttpGet]
        public JsonResult Get(int Tipo, string Descripcion)
        {
            List<ClienteModel> clientes = new List<ClienteModel>();
            try
            {
                var result = _clienteAppService.ConsultarClientesPorParametros(Tipo, Descripcion);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    clientes = result.Data;
                    var TipoDocumento = _utilidadRepository.GetTipoIdentificaciones();
                    clientes.ForEach(item =>
                    {
                        item.TipoIdentificacion = TipoDocumento.FirstOrDefault(x => x.Codigo == item.TipoIdentificacion)?.Nombre ?? "Tipo no reconocido";
                    });
                }

                return Json(new ResponseToViewDto { Estado = true, Data = clientes });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }
    }
}
