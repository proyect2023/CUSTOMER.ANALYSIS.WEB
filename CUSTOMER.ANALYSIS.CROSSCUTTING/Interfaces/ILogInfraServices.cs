using System;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces
{
    public interface ILogInfraServices
    {
        string AddLog(string origen, string mensaje);
        string AddLog(string origen, Exception ex);
        Task<string> AddLogAsync(string origen, string mensaje);
        Task<string> AddLogAsync(string origen, Exception ex);
    }
}
