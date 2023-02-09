

using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CUSTOMER.ANALYSIS.UI.WEB.SITE.Extensions
{
    public static class StartupExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {


        }

        public static void AddServicesMediate(this IServiceCollection services, IConfiguration Configuration)
        {
            GlobalSettings.DirectorioImagenes = Configuration["DirectorioImagenes"];
            GlobalSettings.TipoAlmacenamiento = Configuration["TipoAlmacenamiento"];

        }

        public static void AddValidatorService(this IServiceCollection services)
        {

        }

        public static void AddLoggerServices(this IServiceCollection services, IConfiguration configuration)
        {
            //string mensaje = "";
            //WebSiteSettings settings = GSUtilities.LeerAppSettings<WebSiteSettings>(typeof(Program), ref mensaje, "appsettings.json");
            //if (settings.LOG.Habilitar)
            //{
            //    switch ((DestinosIO)settings.LOG.Destino)
            //    {
            //        case DestinosIO.Disk:
            //            services.AddSingleton<ILogInfraServices>(_ => new LogDiskInfraServices(settings.LOG.Habilitar, settings.LOG.Disk.Ruta, settings.LOG.Disk.Layout, settings.LOG.Disk.FileName));
            //            break;
            //        default: throw new Exception("Destino LOG no reconocido: " + settings.LOG.Destino);
            //    }

            //    GlobalDiagnosticsContext.Set("Application", DomainParameters.NombreAplicacion);
            //    GlobalDiagnosticsContext.Set("Version", AppConstants.Version);
            //}
        }
    }
}
