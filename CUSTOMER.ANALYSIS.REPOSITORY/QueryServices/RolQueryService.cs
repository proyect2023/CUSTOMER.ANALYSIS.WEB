
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using GS.TOOLS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CUSTOMER.ANALYSIS.INFRA.QUERY.QueryServices
{
    public class RolQueryService : BaseQueryService, IRolQueryService
    {
        private readonly EFContext context;

        public RolQueryService(EFContext context, IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            this.context = context;
        }

        public List<RolesQueryDto> ConsultaRolesXCompania(long idCompania, ref string mensaje)
        {
            try
            {
                using var scope = serviceScopeFactory.CreateScope();
                using var edocQueryContext = scope.ServiceProvider.GetRequiredService<EFContext>();
                return edocQueryContext.ConsultaRolesXCompania(idCompania).ToList();
            }
            catch (Exception ex)
            {
                mensaje = $"Error al consultar Roles por Compania. EX: {ex.Message}";
                return null;
            }
        }

        public List<IdQueryDto> ConsultarRolVentanas(short IdRol, ref string mensaje)
        {
            try
            {
                return context.RolPermiso.Where(x => x.IdRol == IdRol && x.Estado == true).Select(c => new IdQueryDto { Id = (int)(c.IdPermiso ?? 0) }).ToList();
            }
            catch (Exception ex)
            {
                mensaje = string.Format("{0} => {1}", this.GetCaller(), ex.Message);
                return null;
            }
        }
    }
}
