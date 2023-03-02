using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        private readonly EFContext _context;

        public ClienteRepository(EFContext context) : base(context)
        {
            this._context = context;
        }

        public List<Cliente>? GetClientes() 
        {
            return _context.Cliente?
                .Include(x => x.IdSectorNavigation)
                .Where(x => x.Estado == true)?.ToList();
        }

        public List<ClientePlan> GetClientePlan(DateTime fechaInicio, DateTime fechaFin, EstadoPlan EstadoPlan)
        {
            return _context.ClientePlans
                .Include(x => x.IdPlanNavigation)
                .Include(x => x.IdPlanNavigation).ThenInclude(x => x.IdTipoPlanNavigation)
                .Include(x => x.IdClienteNavigation)
                .Include(x => x.IdClienteNavigation).ThenInclude(x => x.IdSectorNavigation)
                .Where(x =>
                    (
                        x.Estado == APPLICATION.CORE.Contants.EstadoPlan.Activo ||
                        x.Estado == APPLICATION.CORE.Contants.EstadoPlan.Cambiado
                    )
                    && (x.FechaContratacion >= fechaInicio && x.FechaContratacion <= fechaFin)
                    && (EstadoPlan == 0 || x.Estado == EstadoPlan) 
                //&& (!EstadoCliente || (x.IdClienteNavigation.Estado ?? false))
                ).ToList();
        }


    }
}
