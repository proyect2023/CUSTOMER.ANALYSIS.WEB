using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Models;
using CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using GS.TOOLS;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using Luilliarcec.Identification.Ecuador;
using System.Runtime.ConstrainedExecution;

namespace CUSTOMER.ANALYSIS.WEB.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(VentanasSoporte.Usuarios)]
    public class UsuariosController : BaseController
    {
        private readonly IUsuarioAppService usuarioAppService;
        private readonly IRolRepository rolRepository;


        public UsuariosController(
            ILogInfraServices logInfraServices,
            IUsuarioAppService usuarioAppService,
            IRolRepository rolRepository) : base(logInfraServices)
        {
            this.usuarioAppService = usuarioAppService;
            this.rolRepository = rolRepository;
        }

        // GET: Usuarios
        public IActionResult Index()
        {
            try
            {
                var result = usuarioAppService.ConsultarUsuarios();
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    return View(result.Data);
                }
            }
            catch (Exception ex)
            {
                RegistrarLogError(this.GetCaller(), ex);
            }
            finally
            {
                //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_USUARIO);
            }
            return View();
        }

        // GET: Usuarios/Create
        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                ViewData["rol"] = new SelectList(rolRepository.ObtenerCodigoRol(), "IdRol", "Nombre");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UsuariosAppResultDto crear)
        {
            try
            {
                ViewData["rol"] = new SelectList(rolRepository.ObtenerCodigoRol(), "IdRol", "Nombre");

                if (ModelState.IsValid)
                {
                    if (Identification.ValidateAllTypeIdentification(crear.Cedula) != "05") //cedula
                    {
                        ModelState.AddModelError("Cedula", "La identificación ingresada no corresponde al tipo de identificación CÉDULA");
                        return View(crear);
                    }

                    var usr = GetUserLogin();

                    var result = usuarioAppService.CrearUsuario(crear, usr.IdUsuario, usr.IPLogin);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);
                    if (result.Estado)
                    {
                        //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_USUARIO_CREAR);
                        TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Usuario registrado con éxito");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        if (result.CodigoError == DomainConstants.ERROR_USUARIO_REGISTRADO_MAIL)
                        {
                            ModelState.AddModelError("CorreoElectronico", result.Mensaje);
                        }
                        else if (result.CodigoError == DomainConstants.ERROR_USUARIO_REGISTRADO_USERNAME)
                        {
                            ModelState.AddModelError("Usuario", result.Mensaje);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL ) + RegistrarLogError(this.GetCaller(), ex));
            }
            
            return View(crear);
        }

        // GET: Usuarios/Edit
        [HttpGet]
        public IActionResult Edit(string Id)
        {
            try
            {
                ViewData["rol"] = new SelectList(rolRepository.ObtenerCodigoRol(), "IdRol", "Nombre");

                long Idc = long.Parse(Crypto.DescifrarId(Id));
                var result = usuarioAppService.ObtenerUsuario(Idc);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    return View(result.Data);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }
            finally
            {
                //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_USUARIO_VER, $"Id registro = [{Crypto.DescifrarId(Id)}]");
            }
            return View();
        }

        // POST: Usuarios/Edit
        [HttpPost]  
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UsuariosAppResultDto editar)
        {
            try
            {
                ViewData["rol"] = new SelectList(rolRepository.ObtenerCodigoRol(), "IdRol", "Nombre");

                if (ModelState.IsValid)
                {
                    if (Identification.ValidateAllTypeIdentification(editar.Cedula) != "05") //cedula
                    {
                        ModelState.AddModelError("Cedula", "La identificación ingresada no corresponde al tipo de identificación CÉDULA");
                        return View(editar);
                    }

                    var usr = GetUserLogin();

                    var result = usuarioAppService.EditarUsuario(editar, usr.IdUsuario, usr.IPLogin);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);
                    if (result.Estado)
                    {
                        //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_USUARIO_EDITAR, $"Id registro = {editar.IdUsuario}");
                        TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Usuario actualizado con éxito");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        if (result.CodigoError == DomainConstants.ERROR_USUARIO_REGISTRADO_MAIL)
                        {
                            ModelState.AddModelError("CorreoElectronico", result.Mensaje);
                        }
                        else if (result.CodigoError == DomainConstants.ERROR_USUARIO_REGISTRADO_USERNAME)
                        {
                            ModelState.AddModelError("Usuario", result.Mensaje);
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL ) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(editar);
        }


        [HttpPost]
        public JsonResult Eliminar(string Id)
        {
            try
            {
                var usr = GetUserLogin();
                string IdCifrado = Crypto.DescifrarId(Id);

                (var result, string error) = usuarioAppService.EliminarUsuario(IdCifrado, usr.IdUsuario, usr.IPLogin);
                if (!string.IsNullOrEmpty(error)) throw new Exception(error);

                if (result)
                {
                    //RegistrarActividad(CodigoActividad.COD_SEGURIDAD_USUARIO_ELIMINAR, $"Id registro = [{Crypto.DescifrarId(Id)}]");
                    return Json(new ResponseToViewDto { Estado = true, Mensaje = "Usuario eliminado con éxito" });
                }
                else
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = "Error al eliminar el usuario" });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL ) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }
    }
}
