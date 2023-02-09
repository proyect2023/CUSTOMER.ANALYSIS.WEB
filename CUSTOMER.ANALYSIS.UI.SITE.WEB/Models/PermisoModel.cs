
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using System.ComponentModel.DataAnnotations;

namespace CUSTOMER.ANALYSIS.UI.WEB.SITE.Models
{
    public class PermisoModel
    {
        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string NombreAbreviado { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public long Codigo { get; set; }

        public string Icono { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string Descripcion { get; set; }

        public string Url { get; set; }

        public long? IdPadre { get; set; }
        public string Id { get; set; }

        
    }
}
