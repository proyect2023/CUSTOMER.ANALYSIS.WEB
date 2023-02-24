using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.DomainServices
{
    public interface IAnalisisDomainService
    {
        MethodResponseDto ConsultarParametros();
        MethodResponseDto GuardarParametro(ParametroDto parametro);
    }
}
