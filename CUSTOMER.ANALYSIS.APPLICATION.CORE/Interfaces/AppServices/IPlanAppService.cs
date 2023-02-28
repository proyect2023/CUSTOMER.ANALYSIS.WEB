using CUSTOMER.ANALYSIS.APPLICATION.CORE.Constants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Models;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IPlanAppService
    {
        MethodResponseDto ConsultarPlanes();

        MethodResponseDto ConsultarPlan(string ID);

        MethodResponseDto CrearPlan(PlanViewModel model);

        MethodResponseDto EditarPlan(PlanViewModel model);

        MethodResponseDto EliminarPlan(string ID, string Ip, long Usuario);
    }
}
