﻿using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices
{
    public interface IAnalisisQueryService
    {
        List<ConsultarTotalesDto> ConsultarTotales(bool masVendidos = false, bool antiguos = false, int estadoClientePlan = 0, int sector = 0);
    }
}
