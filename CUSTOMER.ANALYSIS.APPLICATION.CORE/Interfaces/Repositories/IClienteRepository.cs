using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        List<Cliente> GetClientes();
        List<Cliente>? GetClientes(int Tipo, string Descripcion);
        List<ClientePlan> GetClientePlan(DateTime fechaInicio, DateTime fechaFin, EstadoPlan EstadoPlan);
    }
}
