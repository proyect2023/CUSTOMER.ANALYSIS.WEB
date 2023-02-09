using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CUSTOMER.ANALYSIS.UI.SITE.WEB.Controllers
{
    [AllowAnonymous]
    public class PagesController : Controller
    {
        [ActionName("pages-404")]
        public ActionResult pages404() => View();

        [ActionName("pages-500")]
        public ActionResult pages500() => View();

        [ActionName("pages-401")]
        public ActionResult pages401() => View("pages-solicitud-permiso");

        [ActionName("solicitud-permiso")]
        public ActionResult solicitudPermiso()
        {
            @ViewBag.MailAdministrador = "";
            return View("pages-solicitud-permiso");
        }
    }
}
