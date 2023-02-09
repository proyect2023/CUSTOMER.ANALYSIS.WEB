using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Extensions;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Parameters;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Settings;
using GS.TOOLS;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(settings.SITIOWEB.LimiteSesion);//30 minutos
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(culture: "es-CO", uiCulture: "es-CO");
                options.SupportedCultures = new CultureInfo[] { new CultureInfo("es-CO") };
                options.SupportedUICultures = new CultureInfo[] { new CultureInfo("es-CO") };

                options.AddInitialRequestCultureProvider(new CustomRequestCultureProvider(async context =>
                {
                    // My custom request culture logic
                    return new ProviderCultureResult("es");
                }));
            });

            Utilidades.SetCultureInfo();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opciones =>
                    {
                        opciones.AccessDeniedPath = "/Home/Forbidden";
                        opciones.LoginPath = "/Account/Login";
                        opciones.LogoutPath = "/Account/Logoff";
                        //opciones.
                        opciones.ExpireTimeSpan = TimeSpan.FromHours(settings.SITIOWEB.LimiteSesion);
                    });


            GlobalSettings.TimeZoneId = settings.SITIOWEB.TimeZoneId;
            WebSiteParameters.ApiKeyGoogle = Configuration["ApiKeyGoogle"];
            WebSiteParameters.WebLimiteConsulta = settings.SITIOWEB.LimiteConsulta;
            WebSiteParameters.WebFooter = string.Format("{0} v{1}", settings.SITIOWEB.Footer, AppConstants.Version).Replace("{AnioActual}", APPLICATION.CORE.Utilities.Utilidades.GetHoraActual().Year.ToString());
            WebSiteParameters.WebReCaptchaClaveSitioWeb = GSCrypto.DescifrarClave(settings.SITIOWEB.Recaptcha.ClaveSitioWeb, DomainConstants.ENCRIPTA_KEY);
            WebSiteParameters.WebReCaptchaClaveComGoogle = GSCrypto.DescifrarClave(settings.SITIOWEB.Recaptcha.ClaveComGoogle, DomainConstants.ENCRIPTA_KEY);
            WebSiteParameters.Version = Guid.NewGuid().ToString();
            services.AddRepositories();
            services.AddServices();
            services.AddLoggerServices();
            services.AddServicesMediate(Configuration);

            GlobalSettings.ConnectionString = $"Server={settings.ConnectionCredentials.DataSource};Database={settings.ConnectionCredentials.InitialCatalog};User Id={settings.ConnectionCredentials.UserId};Password={settings.ConnectionCredentials.Password};TrustServerCertificate=True";

            if (GlobalSettings.ConnectionString == null) throw new Exception("Cadena de conexión inválida");

            services.AddDbContext<EFContext>(options => options.UseSqlServer(GlobalSettings.ConnectionString));

            

            //services.AddProgressiveWebApp();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IInitialData initialData)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Pages/pages-500");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Pages/pages-404";
                    await next();
                }

                if (context.Response.StatusCode == 401)
                {
                    context.Request.Path = "/Pages/pages-solicitud-permiso";
                    await next();
                }
            });

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

            initialData.Start();
        }
    }
}
