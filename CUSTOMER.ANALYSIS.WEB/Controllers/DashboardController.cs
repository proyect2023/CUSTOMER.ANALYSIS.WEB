using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces;
using CUSTOMER.ANALYSIS.WEB.Controllers;
using GS.TOOLS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CUSTOMER.ANALYSIS.WEB.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(CUSTOMER.ANALYSIS.UI.WEB.SITE.Constants.VentanasSoporte.Dashboard)]
    public class DashboardController : BaseController
    {
        private readonly IStorageService _storageService;
        private readonly IAccountAppService _accountAppService;
        private readonly INotificacionAppService _notificacionAppService;

        public DashboardController(
            IStorageService storageService,
            IAccountAppService accountAppService,
            ILogInfraServices logInfraServices,
            INotificacionAppService notificacionAppService
            ) : base(logInfraServices)
        {
            this._storageService = storageService;
            this._accountAppService = accountAppService;
            this._notificacionAppService = notificacionAppService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Menu") == null)
            {
                var usr = GetUserLogin();
                (var menu, string error) = _accountAppService.LoadMenu(usr.IdUsuario);
                if (menu != null)
                {
                    var IdRol = menu.OrderBy(x => x.IdRol).FirstOrDefault()?.IdRol ?? 0;
                    HttpContext.Session.SetString("Menu", JsonConvert.SerializeObject(menu));

                    string imagen = _storageService.ObtenerImagenBase64(GlobalSettings.TipoAlmacenamiento, usr.Foto ?? "", "");

                    HttpContext.Session.SetString("FotoBase64", string.IsNullOrEmpty(imagen) ? AppConstants.SinImagen : imagen);
                }
                else
                {
                    HttpContext.Session.SetString("Rol", JsonConvert.SerializeObject(new List<Menu>()));
                }
            }

            return View();
        }

        public PartialViewResult GetNotificaciones()
        {
            List<NotificacionAppResultDto> notificaciones = new List<NotificacionAppResultDto>();

            try
            {
                var usr = GetUserLogin();
                var result = _notificacionAppService.GetNotificaciones(usr.IdUsuario, false);
                if (result.TieneErrores) throw new Exception(result.MensajeError);

                if (result.Estado)
                {
                    notificaciones = result.Data;
                    HttpContext.Session.SetInt32("CantidadNotificaciones", notificaciones.Count());
                }
            }
            catch (Exception ex)
            {
                RegistrarLogError(this.GetCaller(), ex);
            }

            return PartialView("_notificacion", notificaciones);
        }

        public PartialViewResult MarcarNotificacion(long Id)
        {
            List<NotificacionAppResultDto> notificaciones = new List<NotificacionAppResultDto>();

            try
            {
                var usr = GetUserLogin();
                var resultMarca = _notificacionAppService.MarcarLeido(Id);
                if (resultMarca.TieneErrores) throw new Exception(resultMarca.MensajeError);

                var result = _notificacionAppService.GetNotificaciones(usr.IdUsuario, false);
                if (result.TieneErrores) throw new Exception(result.MensajeError);

                if (result.Estado)
                {
                    notificaciones = result.Data;
                    HttpContext.Session.SetInt32("CantidadNotificaciones", notificaciones.Count());
                }
            }
            catch (Exception ex)
            {
                RegistrarLogError(this.GetCaller(), ex);
            }

            return PartialView("_notificacion", notificaciones);
        }
    }
}
