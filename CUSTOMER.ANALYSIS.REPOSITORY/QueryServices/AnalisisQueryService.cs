using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.QueryServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Utilities;
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

        public List<ConsultarTotalesDto> ConsultarTotales(string[] validaciones, string[] sectores, string[] planes)
        {
            if (validaciones.Length == 0) { validaciones = new string[] { "0" }; }
            if (sectores.Length == 0) { sectores = new string[] { "0" }; }
            if (planes.Length == 0) { planes = new string[] { "0" }; }

            string validacionesCont = Utilidades.ConcatenarValores(validaciones);
            string sectoresCont = Utilidades.ConcatenarValores(sectores); 
            string planesCont = Utilidades.ConcatenarValores(planes);

            return context.ConsultarTotales(validacionesCont, sectoresCont, planesCont).ToList();
        }

    }
}
