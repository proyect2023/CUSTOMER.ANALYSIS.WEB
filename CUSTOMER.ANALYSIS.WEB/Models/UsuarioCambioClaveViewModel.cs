using System.ComponentModel.DataAnnotations;

namespace CUSTOMER.ANALYSIS.UI.WEB.SITE.Models
{
    public class UsuarioCambioClaveViewModel
    {
        //public long IdUsuario { get; set; }
        //public string Usuario { get; set; }
        //public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "La Contraseña Actual es requerida")]
        [DataType(DataType.Password)]
        public string ClaveActualConfirma { get; set; }

        [Required(ErrorMessage = "La Nueva Contraseña es requerida")]
        [DataType(DataType.Password)]
        public string ClaveNueva { get; set; }

        [Required(ErrorMessage = "La Confirmación de la Nueva Contraseña es requerida")]
        [DataType(DataType.Password)]
        [Compare("ClaveNueva", ErrorMessage = "Las contraseñas no coinciden")]
        public string ClaveNuevaConfirma { get; set; }
    }
}
