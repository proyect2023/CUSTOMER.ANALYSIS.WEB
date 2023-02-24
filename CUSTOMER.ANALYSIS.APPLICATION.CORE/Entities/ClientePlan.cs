using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using System;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;

public partial class ClientePlan
{
    public int IdClientePlan { get; set; }

    public int? IdPlan { get; set; }

    public int? IdCliente { get; set; }

    public DateTime? FechaContratacion { get; set; }

    public decimal? PagoMensual { get; set; }

    public EstadoPlan? Estado { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Plan? IdPlanNavigation { get; set; }
}
