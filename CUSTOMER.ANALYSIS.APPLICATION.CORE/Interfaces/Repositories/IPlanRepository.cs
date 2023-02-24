using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IPlanRepository
    {
        List<Plan> GetPlanes();
        List<Plan> GetPlanesClientes();
        int[]? GetAniosClientePlan();
        List<ClientePlan> GetClientePlan(int anio);
    }
}
