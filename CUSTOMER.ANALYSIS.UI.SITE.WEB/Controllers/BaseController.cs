
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices;
using CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    public class BaseController : Controller
    {
        //private readonly ILogActividadService _logActividadService;
        protected readonly ILogInfraServices _logInfraServices;

        public BaseController()
        {

        }

        //protected BaseController(
        //    ILogInfraServices logInfraServices,
        //    ILogActividadService logActividadService)
        //{
        //    _logActividadService = logActividadService;
        //    _logInfraServices = logInfraServices;
        //}

        protected BaseController(ILogInfraServices logInfraServices)
        {
            _logInfraServices = logInfraServices;
        }

        protected LoginAppResultDto GetUserLogin()
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData);
            return JsonConvert.DeserializeObject<LoginAppResultDto>(userClaim.Value);
        }

        protected string RegistrarLogError(string origen, Exception ex, bool soloCodigo = false)
        {
            if (soloCodigo)
            {
                return _logInfraServices.AddLog(origen, ex);
            }
            else
            {
                return ". Código de seguimiento: " + _logInfraServices.AddLog(origen, ex);
            }
        }

        protected string RegistrarLogError(string origen, string error, bool soloCodigo = false)
        {
            if (soloCodigo)
            {
                return _logInfraServices.AddLog(origen, error);
            }
            else
            {
                return ". Código de seguimiento: " + _logInfraServices.AddLog(origen, error);
            }
        }

        //protected void RegistrarActividad(string CodigoActividad, string MensajeAdicional = "")
        //{
        //    try
        //    {
        //        var usuario = GetUserLogin();
        //        _logActividadService.RegistrarLogActividad(CodigoActividad, usuario.IdUsuario, usuario.IPLogin, MensajeAdicional);
        //    }
        //    catch (Exception ex)
        //    {
        //        //string codigo = ". Código de seguimiento: " + logInfraServices.AddLog($"Error guardar Log Actividad código [{CodigoActividad}]", ex);
        //    }
        //}

        //protected void RegistrarActividad(string CodigoActividad, long IdUsuario, string Ip, string MensajeAdicional = "")
        //{
        //    try
        //    {
        //        _logActividadService.RegistrarLogActividad(CodigoActividad, IdUsuario, Ip, MensajeAdicional);
        //    }
        //    catch (Exception ex)
        //    {
        //        //string codigo = ". Código de seguimiento: " + logInfraServices.AddLog($"Error guardar Log Actividad código [{CodigoActividad}]", ex);
        //    }
        //}
    }
}
