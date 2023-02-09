using System.ComponentModel.DataAnnotations;

namespace CUSTOMER.ANALYSIS.UI.WEB.SITE.Models
{
    public class UsuarioRecuperaClaveViewModel
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "El correo es requerido")]
        public string CorreoElectronico { get; set; }
    }
}
