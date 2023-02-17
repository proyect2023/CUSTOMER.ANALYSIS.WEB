using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.INFRA.QUERY.QueryServices;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.REPOSITORY.QueryServices
{
    public class AnalisisQueryService : IAnalisisQueryService
    {
        private readonly EFContext context;

        public AnalisisQueryService(EFContext context)
        {
            this.context = context;
        }

        public List<ConsultarTotalesDto> ConsultarTotales(bool masVendidos = false, bool antiguos = false, int estadoClientePlan = 0)
        {
            return context.ConsultarTotales(masVendidos, antiguos, estadoClientePlan).ToList();
        }

    }
}
