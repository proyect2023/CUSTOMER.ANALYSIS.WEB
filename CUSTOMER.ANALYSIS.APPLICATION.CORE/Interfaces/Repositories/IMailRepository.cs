using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;

namespace CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IMailRepository : IRepository<Correo>
    {
        List<Correo> ConsultarCorreosNoEnviados();
        bool AddMail(Correo correo);
        List<Correo> ConsultarCorreosReenviados();
        List<Correo> ConsultarErroresDeReenvio();
    }
}
