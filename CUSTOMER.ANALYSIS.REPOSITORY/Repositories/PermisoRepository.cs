
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;

namespace CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories
{
    public class PermisoRepository : Repository<Permisos>, IPermisoRepository
    {
        public PermisoRepository(EFContext context) : base(context)
        {
        }

    }
}
