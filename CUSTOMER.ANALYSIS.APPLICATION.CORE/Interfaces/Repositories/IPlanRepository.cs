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
        Plan? Get(int Id);
        Plan? Get(string Id);
        int AddPlan(Plan plan);
        int UpdatePlan(Plan plan);
        List<Plan> GetPlanesClientes(bool EstadoCliente = false);
        int[]? GetAniosClientePlan();
        List<ClientePlan> GetClientePlan(int anio, bool EstadoCliente = false);
    }
}
