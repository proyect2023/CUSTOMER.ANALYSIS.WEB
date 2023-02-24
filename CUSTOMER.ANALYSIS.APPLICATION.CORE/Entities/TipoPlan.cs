using System;
using System.Collections.Generic;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;

public partial class TipoPlan
{
    public int IdTipoPlan { get; set; }

    public string? Nombre { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Plan> Plans { get; } = new List<Plan>();
}
