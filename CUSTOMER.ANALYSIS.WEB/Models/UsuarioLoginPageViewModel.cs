using System.ComponentModel.DataAnnotations;

namespace CUSTOMER.ANALYSIS.UI.WEB.SITE.Models
{
    public class UsuarioLoginPageViewModel
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Usuario { get; set; }
        
        [Required(ErrorMessage = "La clave es requerida")]
        public string Clave { get; set; }

        //[Required(ErrorMessage = "El NIT es requerido")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "El nit solo acepta valores numéricos")]
        //public string Nit { get; set; }

        [Required(ErrorMessage = "Captcha es requerido")]
        public string Captcha { get; set; }
    }
}
