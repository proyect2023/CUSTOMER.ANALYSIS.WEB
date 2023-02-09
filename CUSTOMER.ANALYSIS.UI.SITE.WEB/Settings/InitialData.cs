
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Parameters;
using CUSTOMER.ANALYSIS.WEB;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.UI.WEB.SITE.Settings
{
    public class InitialData : IInitialData
    {
        private readonly IUtilidadRepository _utilidadRepository;
        private string mensaje = string.Empty;

        public InitialData(IUtilidadRepository utilidadRepository)
        {
            this._utilidadRepository = utilidadRepository;
        }

        public void Start()
        {
            //_utilidadRepository.InicializarDb();

            var settings = GSUtilities.LeerAppSettings<WebSiteSettings>(typeof(InitialData), ref mensaje);

            GlobalSettings.ConfiguracionMailUser = _utilidadRepository.GetParametro("CONFIGURACION_MAIL_USER")?.Valor ?? "";
            GlobalSettings.ConfiguracionMailPassword = _utilidadRepository.GetParametro("CONFIGURACION_MAIL_PASSWORD")?.Valor ?? "";
            GlobalSettings.ConfiguracionMailPort = int.Parse(_utilidadRepository.GetParametro("CONFIGURACION_MAIL_PORT")?.Valor ?? "0");
            GlobalSettings.ConfiguracionMailSsl = (_utilidadRepository.GetParametro("CONFIGURACION_MAIL_SSL")?.Valor ?? "") == "1";
            GlobalSettings.ConfiguracionMailHost = _utilidadRepository.GetParametro("CONFIGURACION_MAIL_HOST")?.Valor ?? "";

            GlobalSettings.LoginAppNumeroIntentoBloqueo = int.Parse(_utilidadRepository.GetParametro("NUMERO_INTENTOS_BLOQUEO")?.Valor ?? "5");
            GlobalSettings.LoginAppDiasForzarCambioPassword = int.Parse(_utilidadRepository.GetParametro("NUMERO_DIAS_FORZAR_CAMBIO_PASSWORD")?.Valor ?? "90");
            GlobalSettings.NumeroDiasReaperturaTicket = int.Parse(_utilidadRepository.GetParametro("NUMERO_DIAS_REAPERTURA_TICKET")?.Valor ?? "30");
            GlobalSettings.NumeroIntentosEnvioBloqueo = int.Parse(_utilidadRepository.GetParametro("NUMERO_INTENTOS_ENVIO_BLOQUEO")?.Valor ?? "5");

            //GlobalSettings.FormatoReporte_RutaBase = settings.FormatoReporte.RutaBase;
            //GlobalSettings.FormatoReporte_NombreArchivo = settings.FormatoReporte.NombreArchivo;
            //GlobalSettings.FormatoReporte_Extension = settings.FormatoReporte.Extension;

            //#region Verificar Existencia de roles
            //var roles = _utilidadRepository.GetRolesPrincipales();
            //if (roles == null || !roles.Any())
            //{
            //    _utilidadRepository.AddRol(((int)Roles.Administrador), Roles.Administrador.ToString());
            //    _utilidadRepository.AddRol(((int)Roles.Empleado), Roles.Empleado.ToString());
            //}
            //else
            //{
            //    if (roles.FirstOrDefault(x => x.Nombre == Roles.Administrador.ToString()) == null)
            //    {
            //        _utilidadRepository.AddRol(((int)Roles.Administrador), Roles.Administrador.ToString());
            //    }

            //    if (roles.FirstOrDefault(x => x.Nombre == Roles.Empleado.ToString()) == null)
            //    {
            //        _utilidadRepository.AddRol(((int)Roles.Empleado), Roles.Empleado.ToString());
            //    }
            //}
            //#endregion

            //#region Crear usuario SuperAdministrador
            //_utilidadRepository.AgregarUsuarioAdministrador();
            //#endregion

            //#region Asignar Permisos SuperAdministrador

            //#endregion
        }
    }
}
