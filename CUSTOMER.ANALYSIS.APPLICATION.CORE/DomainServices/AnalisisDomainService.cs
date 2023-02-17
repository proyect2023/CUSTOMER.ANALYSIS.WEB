using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.DomainServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.DomainServices;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.DomainServices
{
    public class AnalisisDomainService : IAnalisisDomainService
    {
        private readonly IUtilidadRepository _utilidadRepository;

        public AnalisisDomainService(IUtilidadRepository utilidadRepository)
        {
            _utilidadRepository = utilidadRepository;
        }

        public MethodResponseDto ConsultarParametros()
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                responseDto.Data = _utilidadRepository.GetParametros().Select(c => new ParametroDto(c)).ToList();
                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }
    }
}
