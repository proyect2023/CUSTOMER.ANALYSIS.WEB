using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Models;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Models;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.WEB.Controllers
{
    [Authorize]
    [Filters.MenuFilter(VentanasSoporte.Permisos)]
    public class PermisosController : BaseController
    {
        private readonly IPermisoAppService permisoAppService;

        public PermisosController(
            ILogInfraServices logInfraServices, IPermisoAppService permisoAppService) : base(logInfraServices)
        {
            this.permisoAppService = permisoAppService;
        }

        public IActionResult Index()
        {
            return View();
            //try
            //{
            //    (var result, string error) = permisoAppService.ListarPermisos();
            //    if (!string.IsNullOrEmpty(error)) throw new Exception(error);
            //    return View(result);
            //}
            //catch (Exception ex)
            //{
            //    TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL ) + RegistrarLogError(this.GetCaller(), ex));
            //    return View(new List<APPLICATION.CORE.DTOs.AppServices.PermisoAppResultDto>());
            //}
            //finally
            //{
            //    RegistrarActividad(CodigoActividad.COD_SEGURIDAD_PERMISOS);
            //}
        }
        
        public IActionResult Get()
        {
            try
            {
                (var result, string error) = permisoAppService.ListarPermisos();
                if (!string.IsNullOrEmpty(error))
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = error });
                }
                return Json(new ResponseToViewDto { Estado = true, Data = result });
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
            finally
            {
                //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_PERMISOS);
            }
        }

        public IActionResult Registrar(string Id)
        {
            PermisoViewModel model = new PermisoViewModel();
            try
            {
                (var permisos, string errorGet) = permisoAppService.VerPermisos();
                if (!string.IsNullOrEmpty(errorGet)) throw new Exception(errorGet);

                ViewBag.VentanasPadre = new SelectList(permisos.Select(c => new { IdPadre = c.Item1, Nombre = c.Item2 } ), "IdPadre", "Nombre");
                if (!string.IsNullOrEmpty(Id))
                {
                    (var result, string error) = permisoAppService.VerPermiso(Id);
                    if (!string.IsNullOrEmpty(error))
                    {
                        throw new Exception(error);
                    }
                    else
                    {
                        //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_PERMISOS_VER, $"Id registro = [{Crypto.DescifrarId(Id)}]");
                        model = new PermisoViewModel
                        {
                            Codigo = result.Codigo,
                            Descripcion = result.Descripcion,
                            Icono = result.Icono,
                            IdPadre = result.IdPadre,
                            Id = result.Id,
                            NombreAbreviado = result.NombreAbreviado,
                            Url = result.Url,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL ) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(PermisoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usr = GetUserLogin();
                    
                    if (!string.IsNullOrEmpty(model.Id))
                    {
                        (var result, string error) = permisoAppService.EditarPermiso(
                            model.Id,
                            model.NombreAbreviado,
                            model.Codigo,
                            model.Icono,
                            model.Descripcion,
                            model.Url,
                            model.IdPadre,
                            usr.IdUsuario,
                            usr.IPLogin
                            );
                        if (!string.IsNullOrEmpty(error))
                        {
                            ModelState.AddModelError(string.Empty, error);
                        }

                        if (result)
                        {
                            //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_PERMISOS_EDITAR, $"Id registro = [{Crypto.DescifrarId(model.Id)}]");
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Permiso actualizado con éxito");
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        (var result, string error) = permisoAppService.AgregarPermiso(
                            model.NombreAbreviado,
                            model.Codigo,
                            model.Icono,
                            model.Descripcion,
                            model.Url,
                            model.IdPadre,
                            usr.IdUsuario,
                            usr.IPLogin
                            );
                        if (!string.IsNullOrEmpty(error))
                        {
                            ModelState.AddModelError(string.Empty, error);
                        }
                        if (result)
                        {
                            //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_PERMISOS_CREAR);
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Permiso registrado con éxito");
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL ) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Eliminar(string Id)
        {
            try
            {
                var usr = GetUserLogin();
                (var result, string error) = permisoAppService.EliminarPermiso(Id, usr.IdUsuario, usr.IPLogin);
                if (!string.IsNullOrEmpty(error)) throw new Exception(error);

                if (result)
                {
                    //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_PERMISOS_ELIMINAR, $"Id registro = [{Crypto.DescifrarId(Id)}]");
                    return Json(new ResponseToViewDto { Estado = true, Mensaje = "Permiso eliminado con éxito" });
                }
                else
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = "Error al eliminar el permiso" });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }
    }
}
