using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Models
{
    public class PlanViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public int? IdTipoPlan { get; set; }
        
        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string? Nombre { get; set; }
        
        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public string? Precio { get; set; }
        
        [Required(ErrorMessage = DomainConstants.MENSAJE_CAMPO_REQUIRED)]
        public int? Velocidad { get; set; }


        public string? TipoPlan { get; set; }

        public string? Ip { get; set; }
        public long Usuario { get; set; }

        public PlanViewModel(Plan plan)
        {
            Id = Utilities.Crypto.CifrarId(plan.IdPlan);
            IdTipoPlan = plan.IdTipoPlan ?? 0;
            TipoPlan = plan.IdTipoPlanNavigation?.Nombre ?? "";
            Nombre = plan.Nombre;
            Precio = Utilities.Utilidades.DoubleToString_FrontCO(plan.Precio, 2);
            Velocidad = plan.Velocidad;

        }
        
        public PlanViewModel()
        {

        }


    }
}
