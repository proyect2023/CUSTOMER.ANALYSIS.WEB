using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Extensions;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Parameters;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Settings;
using GS.TOOLS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CUSTOMER.ANALYSIS.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddSignalR();
            string mensaje = null;
            WebSiteSettings settings = GSUtilities.LeerAppSettings<WebSiteSettings>(typeof(Program), ref mensaje, "appsettings.json");

            Utilidades.SetCultureInfo();

            GlobalSettings.TimeZoneId = settings.SITIOWEB.TimeZoneId;
            WebSiteParameters.WebLimiteConsulta = settings.SITIOWEB.LimiteConsulta;
            WebSiteParameters.WebFooter = string.Format("{0} v{1}", settings.SITIOWEB.Footer, AppConstants.Version).Replace("{AnioActual}", APPLICATION.CORE.Utilities.Utilidades.GetHoraActual().Year.ToString());
            WebSiteParameters.WebReCaptchaClaveSitioWeb = GSCrypto.DescifrarClave(settings.SITIOWEB.Recaptcha.ClaveSitioWeb, DomainConstants.ENCRIPTA_KEY);
            WebSiteParameters.WebReCaptchaClaveComGoogle = GSCrypto.DescifrarClave(settings.SITIOWEB.Recaptcha.ClaveComGoogle, DomainConstants.ENCRIPTA_KEY);
            WebSiteParameters.Version = Guid.NewGuid().ToString();
            services.AddRepositories();
            services.AddServices();

            GlobalSettings.ConnectionString = $"Server={settings.ConnectionCredentials.DataSource};Database={settings.ConnectionCredentials.InitialCatalog};User Id={settings.ConnectionCredentials.UserId};Password={settings.ConnectionCredentials.Password};TrustServerCertificate=True";

            if (GlobalSettings.ConnectionString == null) throw new Exception("Cadena de conexión inválida");

            services.AddDbContext<EFContext>(options => options.UseSqlServer(GlobalSettings.ConnectionString));

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(settings.SITIOWEB.LimiteSesion);//30 minutos
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //services.AddProgressiveWebApp();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            //Habilitar sesion
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Dashboard}/{action=Index}/{id?}");
            });
        }
    }
}
