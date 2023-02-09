using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
using CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Extensions;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Parameters;
using CUSTOMER.ANALYSIS.UI.WEB.SITE.Settings;
using GS.TOOLS;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            try
            {
                var services = builder.Services;
                // Add services to the container.
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

                using ServiceProvider serviceProvider = services.BuildServiceProvider();
                using IServiceScope scope = serviceProvider.CreateScope();

                services.AddRepositories();
                services.AddServices();
                services.AddLoggerServices();
                services.AddServicesMediate(scope.ServiceProvider.GetRequiredService<IConfiguration>());


                GlobalSettings.TimeZoneId = settings.SITIOWEB.TimeZoneId;

                WebSiteParameters.WebLimiteConsulta = settings.SITIOWEB.LimiteConsulta;
                WebSiteParameters.WebFooter = string.Format("{0} v{1}", settings.SITIOWEB.Footer, AppConstants.Version).Replace("{AnioActual}", APPLICATION.CORE.Utilities.Utilidades.GetHoraActual().Year.ToString());
                WebSiteParameters.WebReCaptchaClaveSitioWeb = GSCrypto.DescifrarClave(settings.SITIOWEB.Recaptcha.ClaveSitioWeb, DomainConstants.ENCRIPTA_KEY);
                WebSiteParameters.WebReCaptchaClaveComGoogle = GSCrypto.DescifrarClave(settings.SITIOWEB.Recaptcha.ClaveComGoogle, DomainConstants.ENCRIPTA_KEY);
                WebSiteParameters.Version = Guid.NewGuid().ToString();


                GlobalSettings.ConnectionString = $"Server={settings.ConnectionCredentials.DataSource};Database={settings.ConnectionCredentials.InitialCatalog};User Id={settings.ConnectionCredentials.UserId};Password={settings.ConnectionCredentials.Password};TrustServerCertificate=True";

                if (GlobalSettings.ConnectionString == null) throw new Exception("Cadena de conexión inválida");

                services.AddDbContext<EFContext>(options => options.UseSqlServer(GlobalSettings.ConnectionString));

                iniciarDatos();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            var app = builder.Build();

            try
            {
                //if (app.Environment.IsDevelopment())
                //{
                //    app.UseDeveloperExceptionPage();
                //}
                //else
                //{
                //    app.UseExceptionHandler("/Pages/pages-500");
                //    app.UseHsts();
                //}

                if (!app.Environment.IsDevelopment())
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

                //app.MapHub<EscanerCodigoAreteHub>("/EscanerCodigoAreteHub");

                app.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Dashboard}/{action=Index}/{id?}");

                //app.UseSqlTableDependency<SubscribeTableDependencyCodigoArete>();

                app.Run();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void iniciarDatos()
        {
            IUtilidadRepository utilidadRepository = new UtilidadRepository(new EFContext());
            IInitialData initialData = new InitialData(utilidadRepository);
            initialData.Start();


        }
    }
}