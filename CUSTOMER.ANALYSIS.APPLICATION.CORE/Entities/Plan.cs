using System;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;

public partial class Plan
{
    public int IdPlan { get; set; }

    public int? IdTipoPlan { get; set; }

    public string? Nombre { get; set; }

    public decimal? Precio { get; set; }

    public int? Velocidad { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<ClientePlan> ClientePlans { get; } = new List<ClientePlan>();

    public virtual TipoPlan? IdTipoPlanNavigation { get; set; }
}
