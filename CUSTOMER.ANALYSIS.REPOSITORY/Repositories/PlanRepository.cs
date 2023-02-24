using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.REPOSITORY.Repositories
{
    public class PlanRepository : IPlanRepository
    {
        private readonly EFContext _context;

        public PlanRepository(EFContext context)
        {
            _context = context;
        }

        public List<Plan> GetPlanes()
        {
            return _context.Plans.Where(x => x.Estado == true).ToList();
        }

        public int[]? GetAniosClientePlan()
        {
            var result = _context.ClientePlans
                .Where(x => x.Estado == APPLICATION.CORE.Contants.EstadoPlan.Activo || x.Estado == APPLICATION.CORE.Contants.EstadoPlan.Cambiado && x.FechaContratacion != null)
                .GroupBy(x => x.FechaContratacion.Value.Year)
                .Select(x => x.Key)
                .ToList();

            return result?.OrderByDescending(x => x).Select(x => x).ToArray();
        }
        
        public List<Plan> GetPlanesClientes()
        {
            return _context.Plans
                .Include(x => x.ClientePlans)
                .Include(x => x.ClientePlans).ThenInclude(x => x.IdClienteNavigation)
                .Where(x => x.Estado == true).ToList();
        }

        public List<ClientePlan> GetClientePlan(int anio)
        {
            return _context.ClientePlans
                .Include(x => x.IdPlanNavigation)
                .Include(x => x.IdClienteNavigation)
                .Where(x => (x.Estado == APPLICATION.CORE.Contants.EstadoPlan.Activo || x.Estado == APPLICATION.CORE.Contants.EstadoPlan.Cambiado) && x.FechaContratacion.Value.Year == anio).ToList();
        }


    }
}
