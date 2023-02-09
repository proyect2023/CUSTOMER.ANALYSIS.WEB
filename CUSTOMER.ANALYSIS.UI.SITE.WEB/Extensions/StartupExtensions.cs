

using CUSTOMER.ANALYSIS.APPLICATION.CORE.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DomainServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.DomainServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Parameters;
using CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces;
using CUSTOMER.ANALYSIS.CROSSCUTTING.LOG.Services;
using CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories;
using CUSTOMER.ANALYSIS.INFRA.QUERY.QueryServices;
using CUSTOMER.ANALYSIS.INFRA.SERVICE.MAIL.Services;
using CUSTOMER.ANALYSIS.INFRA.SERVICE.STORAGE.Services;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Parameters;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Settings;
using CUSTOMER.ANALYSIS.WEB;
using GS.IO.Constants;
using GS.TOOLS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using System;

namespace CUSTOMER.ANALYSIS.UI.WEB.SITE.Extensions
{
    public static class StartupExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IMailRepository, MailRepository>();
            services.AddScoped<INotificacionRepository, NotificacionRepository>();
            services.AddScoped<IPermisoRepository, PermisoRepository>();
            //services.AddScoped<IRepository, Repository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUtilidadRepository, UtilidadRepository>();
            services.AddScoped<IPortalQueryService, PortalQueryService>();
            services.AddScoped<IRolQueryService, RolQueryService>();
            //services.AddScoped<IUsuarioQueryService, UsuarioQueryService>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, DiskStorageService>();
            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<IMailDomainService, MailDomainService>();
            services.AddScoped<IEnvioMail, EnvioMail>();
            services.AddScoped<INotificacionAppService, NotificacionAppService>();
            services.AddScoped<IRolAppService, RolAppService>();
            services.AddScoped<IUsuarioAppService, UsuarioAppService>();
            services.AddScoped<IPermisoAppService, PermisoAppService>();
            services.AddScoped<IInitialData, InitialData>();


        }

        public static void AddServicesMediate(this IServiceCollection services, IConfiguration Configuration)
        {
            GlobalSettings.DirectorioImagenes = Configuration["DirectorioImagenes"];
            GlobalSettings.TipoAlmacenamiento = Configuration["TipoAlmacenamiento"];
            WebSiteParameters.ApiKeyGoogle = Configuration["ApiKeyGoogle"];
        }

        public static void AddValidatorService(this IServiceCollection services)
        {

        }

        public static void AddLoggerServices(this IServiceCollection services)
        {
            string mensaje = "";
            WebSiteSettings settings = GSUtilities.LeerAppSettings<WebSiteSettings>(typeof(StartupExtensions), ref mensaje, "appsettings.json");
            if (settings.LOG.Habilitar)
            {
                switch ((DestinosIO)settings.LOG.Destino)
                {
                    case DestinosIO.Disk:
                        services.AddSingleton<ILogInfraServices>(_ => new LogDiskInfraServices(settings.LOG.Habilitar, settings.LOG.Disk.Ruta, settings.LOG.Disk.Layout, settings.LOG.Disk.FileName));
                        break;
                    default: throw new Exception("Destino LOG no reconocido: " + settings.LOG.Destino);
                }

                GlobalDiagnosticsContext.Set("Application", DomainParameters.NombreAplicacion);
                GlobalDiagnosticsContext.Set("Version", AppConstants.Version);
            }
        }
    }
}
