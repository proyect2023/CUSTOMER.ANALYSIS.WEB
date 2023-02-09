using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.WEB.Controllers
{
    public class DashboardController : Controller
    {
        
        public IActionResult Index()
        {
            List<Menu> menu = new List<Menu>
            {
                new Menu
                {
                    Label = "Cliente",
                    Icon = "fa fa-users",
                    Url = "Cliente/",
                    SubMenu = new List<Menu>
                    {

                    }
                }

            };

            HttpContext.Session.SetString("Menu", JsonConvert.SerializeObject(menu));

            return View();
        }
    }
}
