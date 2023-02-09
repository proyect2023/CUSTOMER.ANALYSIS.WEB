using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.AppServices
{
    public sealed class LoginInternoAppResultDto
    {
        //public long IdCompania { get; set; }
        public long IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string ClaveEncriptada { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string NomUsrDirAct { get; set; }
        public string GrupoUsuarioDirActivo { get; set; }
        public string NitCiaNube { get; set; }
        public bool ForzarCambioClave { get; set; }
        public string IPLogin { get; set; }
        public string MenuPersonalizado { get; set; }
        public string VentanasActivasConcat { get; set; }
        public bool Bloqueado { get; set; }
        public bool Autorizado { get; set; }
        public List<Menu> Menu { get; set; }
        
    }

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
