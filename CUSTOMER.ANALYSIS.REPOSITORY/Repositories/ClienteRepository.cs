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

    }
}
