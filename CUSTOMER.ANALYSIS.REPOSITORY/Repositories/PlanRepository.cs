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
            return _context.Plans
                .Include(x => x.IdTipoPlanNavigation)
                .Where(x => x.Estado == true).ToList();
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
        
        public List<Plan> GetPlanesClientes(bool EstadoCliente = false)
        {
            return _context.Plans
                .Include(x => x.ClientePlans)
                .Include(x => x.ClientePlans.Where(x => (!EstadoCliente || (x.IdClienteNavigation.Estado ?? false) ))).ThenInclude(x => x.IdClienteNavigation)
                .Where(x =>  x.Estado == true).ToList();
        }

        public List<ClientePlan> GetClientePlan(int anio, bool EstadoCliente = false)
        {
            return _context.ClientePlans
                .Include(x => x.IdPlanNavigation)
                .Include(x => x.IdClienteNavigation)
                .Where(x => 
                    (
                        x.Estado == APPLICATION.CORE.Contants.EstadoPlan.Activo || 
                        x.Estado == APPLICATION.CORE.Contants.EstadoPlan.Cambiado
                    ) 
                    && x.FechaContratacion.Value.Year == anio
                    && (!EstadoCliente || (x.IdClienteNavigation.Estado ?? false))
                ).ToList();
        }

        public Plan? Get(int Id)
        {
            return _context.Plans.FirstOrDefault(x => x.IdPlan == Id);
        }

        public Plan? Get(string Id)
        {
            return _context.Plans.FirstOrDefault(x => x.Nombre == Id);
        }

        public int AddPlan(Plan plan)
        {
            _context.Plans.Add(plan);
            return _context.SaveChanges();
        }
        
        public int UpdatePlan(Plan plan)
        {
            _context.Plans.Update(plan);
            return _context.SaveChanges();
        }

    }
}
