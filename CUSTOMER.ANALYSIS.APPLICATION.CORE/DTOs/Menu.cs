using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs
{
    public class Menu
    {
        public Menu()
        {
            SubMenu = new List<Menu>();
        }
        public string Label { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public long IdRol { get; set; }
        public string Rol { get; set; }
        public int Order { get; set; }
        public List<Menu> SubMenu { get; set; }
    }
}
