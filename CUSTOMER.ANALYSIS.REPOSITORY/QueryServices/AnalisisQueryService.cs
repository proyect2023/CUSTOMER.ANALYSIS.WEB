using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;

namespace CUSTOMER.ANALYSIS.REPOSITORY.QueryServices
{
    public class AnalisisQueryService : IAnalisisQueryService
    {
        private readonly EFContext context;

        public AnalisisQueryService(EFContext context)
        {
            this.context = context;
        }

        public List<ConsultarTotalesDto> ConsultarTotales(bool masVendidos = false, bool antiguos = false, int estadoClientePlan = 0, int sector = 0)
        {
            return context.ConsultarTotales(masVendidos, antiguos, estadoClientePlan, sector).ToList();
        }

    }
}
