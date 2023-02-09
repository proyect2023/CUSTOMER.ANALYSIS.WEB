using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using System.Collections.Generic;
using System.Linq;

namespace CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly EFContext _context;

        public ClienteRepository(EFContext context)
        {
            this._context = context;
        }

        public List<Cliente> GetAll() 
        {
            return _context.Cliente.Where(x => x.Estado == true).ToList();
        }

    }
}
